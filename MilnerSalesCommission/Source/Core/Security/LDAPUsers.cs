// Copyright 2016-2017, Milner Technologies, Inc.
//
// This document contains data and information proprietary to
// Milner Technologies, Inc.  This data shall not be disclosed,
// disseminated, reproduced or otherwise used outside of the
// facilities of Milner Technologies, Inc., without the express
// written consent of an officer of the corporation.
//

using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Principal;
using System.Runtime.Caching;
using System.Data;
using System.Linq;
using System.Diagnostics;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

namespace Security
{
    public sealed class LDAPUsers
    {
        #region Enum Declarations

        /// <summary>
        /// States for LDAP membership check.
        /// </summary>
        private enum TLdapStatus
        {
            /// <summary>
            /// LDAP membership is unknown.
            /// </summary>
            Check,

            /// <summary>
            /// This machine is a member of a domain.
            /// </summary>
            Ldap,

            /// <summary>
            /// This machine is a member of a workgroup.
            /// </summary>
            Workgroup
        }

        /// <summary>
        /// Bit-wise values available for DirectoryEntry Properties[] as defined by Microsoft.
        /// </summary>
        private enum TUserAccountControl : int
        {
            Script = 1,
            AccountDisabled = 2,
            HomeDirectoryRequired = 8,
            AccountLockedOut_DEPRECATED = 16,
            PasswordNotRequired = 32,
            PasswordCannotChange_DEPRECATED = 64,
            EncryptedTextPasswordAllowed = 128,
            TempDuplicateAccount = 256,
            NormalAccount = 512,
            InterDomainTrustAccount = 2048,
            WorkstationTrustAccount = 4096,
            ServerTrustAccount = 8192,
            PasswordDoesNotExpire = 65536,
            MnsLogonAccount = 131072,
            SmartCardRequired = 262144,
            TrustedForDelegation = 524288,
            AccountNotDelegated = 1048576,
            UseDesKeyOnly = 2097152,
            DontRequirePreauth = 4194304,
            PasswordExpired_DEPRECATED = 8388608,
            TrustedToAuthenticateForDelegation = 16777216,
            NoAuthDataRequired = 33554432
        }

        #endregion

        #region Private and Public Declarations

        /// <summary>
        /// Indicates whether or not this machine is a member of a domain.
        /// </summary>
        private static TLdapStatus s_IsLdap = TLdapStatus.Check;

        //To get domain name
        private string m_domainName = Environment.UserDomainName;
        //To assign integrity level SIDs
        private List<string> m_integritylevelSIDs = new List<string>() { "S-1-16-4096", "S-1-16-8192", "S-1-16-12288", "S-1-16-16384" };

        #endregion

        #region Constructor
        //Constructor
        public LDAPUsers()
        {
            if (s_IsLdap == TLdapStatus.Check)
            {
                if (CheckLdap())
                {
                    s_IsLdap = TLdapStatus.Ldap;
                }
                else
                {
                    s_IsLdap = TLdapStatus.Workgroup;
                }
            }
        }

        #endregion 

        #region Private and Public Methods

        /// <summary>
        /// To Get Users accounts from the active directory
        /// </summary>
        /// <returns>The list of all users and groups in the domain.</returns>
        /// 
        /// <remarks>This list is too large to use on big domains.  Do not use this method.</remarks>
        public List<string> GetUserAccounts(string domainName = null)
        {
            if (!string.IsNullOrWhiteSpace(domainName))
            {
                m_domainName = domainName;
            }

            if (s_IsLdap == TLdapStatus.Workgroup)
            {
                return GetUserAccountsNonDomain();
            }

            List<string> userList = new List<string>();
            //To get Users list from the AD Server
            using (PrincipalContext ADServer = new PrincipalContext(ContextType.Domain, m_domainName))
            {
                using (UserPrincipal insUserPrincipal = new UserPrincipal(ADServer))
                {
                    using (PrincipalSearcher insPrincipalSearcher = new PrincipalSearcher(insUserPrincipal))
                    {
                        (insPrincipalSearcher.GetUnderlyingSearcher() as DirectorySearcher).PageSize = 500;

                        using (PrincipalSearchResult<Principal> users = insPrincipalSearcher.FindAll())
                        {
                            foreach (Principal principle in users)
                            {
                                if (principle != null)
                                {
                                    try
                                    {
                                        if (IsValidIdentity(principle.SamAccountName))
                                        {
                                            userList.Add(ConstructMemberInfo(principle.SamAccountName.ToLower(), m_domainName, principle.StructuralObjectClass));
                                        }
                                    }
                                    finally
                                    {
                                        principle.Dispose();
                                    }
                                }
                            }
                        }
                    }
                }

            }

            //To get groups list from the AD Server
            using (PrincipalContext ADServer = new PrincipalContext(ContextType.Domain, m_domainName))
            {
                using (GroupPrincipal insGroupPrincipal = new GroupPrincipal(ADServer))
                {
                    using (PrincipalSearcher insGroupPrincipalSearcher = new PrincipalSearcher(insGroupPrincipal))
                    {
                        (insGroupPrincipalSearcher.GetUnderlyingSearcher() as DirectorySearcher).PageSize = 500;

                        using (PrincipalSearchResult<Principal> groups = insGroupPrincipalSearcher.FindAll())
                        {
                            foreach (Principal principle in groups)
                            {
                                if (principle != null)
                                {
                                    try
                                    {
                                        if (IsValidIdentity(principle.SamAccountName))
                                        {
                                            userList.Add(ConstructMemberInfo(principle.SamAccountName.ToLower(), m_domainName, principle.StructuralObjectClass));
                                        }
                                    }
                                    finally
                                    {
                                        principle.Dispose();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            userList.Sort();
            userList.TrimExcess(); 

            return userList;
        }

        /// <summary>
        /// Get a list of users and groups for non-domain machines.
        /// </summary>
        /// <returns></returns>
        public List<string> GetUserAccountsNonDomain()
        {
            List<string> userList = new List<string>();

            try
            {
                string domainName = Environment.UserDomainName.ToLower();

                string bindingString = string.Format("WinNT://{0}", domainName);
                using (DirectoryEntry root = new DirectoryEntry(bindingString))
                {
                    foreach (DirectoryEntry child in root.Children)
                    {
                        if (child.SchemaClassName == "User")
                        {
                            bool valid = true;
                            try
                            {
                                TUserAccountControl acctProps = (TUserAccountControl)(child.Properties["UserFlags"].Value);
                                valid = ((acctProps & TUserAccountControl.AccountDisabled) != TUserAccountControl.AccountDisabled);
                            }
                            catch
                            {
                            }

                            if (valid)
                            {
                                userList.Add(ConstructMemberInfo(child.Name.ToLower(), m_domainName, "user"));
                            }
                        }
                        else if (child.SchemaClassName == "Group")
                        {
                            userList.Add(ConstructMemberInfo(child.Name.ToLower(), m_domainName, "group"));
                        }
                    }
                }

                userList.Sort();
            }
            catch (Exception ex)
            {
                Utility.UtilityLog.EventLogException("Sales Commission", "GetUserAccountsNonDomain exception.", ex);
            }

            return userList;
        }

        /// <summary>
        /// Get the list of groups to which a user account belongs.
        /// </summary>
        /// <param name="userName">The name of the account.</param>
        /// <returns>A list of groups to which the user belongs.</returns>
        public List<string> GetUserGroups(string userName)
        {
            if (s_IsLdap == TLdapStatus.Workgroup)
            {
                return GetUserGroupsFromWorkGroup(userName);
            }
            else
            {
                userName = GetMemberName(userName);
                return GetUserGroupsFromActiveDirectory(userName);
            }

        }


        /// <summary>
        /// To find whether member is user or group
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static bool IsMemberGroup(string userName)
        {
            bool isGroup = false;
            int memberTypeSeperatorIndex = userName.IndexOf("[");
            if (memberTypeSeperatorIndex != -1)
            {
                string userOrGroup = userName.Substring((memberTypeSeperatorIndex)).Replace("[", "").Replace("]", "");
                if (!string.IsNullOrEmpty(userOrGroup))
                {
                    isGroup = userOrGroup.Equals("user", StringComparison.OrdinalIgnoreCase) ? false : true;
                }
            }
            return isGroup;
        }

        internal static string GetMemberDomain(string memberDetails)
        {
            int index = memberDetails.IndexOf("\\");
            string domainName = null;
            if (index != -1)
            {
                domainName = memberDetails.Substring(0, memberDetails.IndexOf("\\"));
            }
            else
            {
                index = memberDetails.IndexOf("@");
                if (index != -1)
                {
                    domainName = memberDetails.Substring(index + 1);
                }
            }
            return domainName != null ? domainName : string.Empty;
        }

        /// <summary>
        /// Get the list of groups to which a user account belongs.
        /// </summary>
        /// <param name="userName">The name of the account.</param>
        /// <returns>A list of groups to which the user belongs.</returns>
        public List<string> GetUserGroups(string userName, bool IsGroup)
        {
            if (s_IsLdap == TLdapStatus.Workgroup)
            {
                return GetUserGroupsFromWorkGroup(userName, IsGroup);
            }
            else
            {
                //userName = GetMemberName(userName);
                return GetUserGroupsFromActiveDirectory(userName, IsGroup);
            }

        }

        /// <summary>
        /// Get the list of authorization groups to which a user belongs from a domain.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="isGroup"></param>
        /// <returns></returns>
        List<string> GetUserGroupsFromActiveDirectory(string userName, bool isGroup = false)
        {
            List<string> userGroupList = null;
            string cacheKey = userName + "GetUserGroupsFromActiveDirectory";
            try
            {
                userGroupList = (List<string>)MemoryCache.Default[cacheKey];
                if (userGroupList != null)
                {
                    return userGroupList;
                }
            }
            catch
            {
            }

            userGroupList = new List<string>();
            try
            {
                string userNameAlone = GetMemberName(userName);
                if (string.IsNullOrWhiteSpace(userNameAlone))
                {
                    throw new Exception("Can't parse user name.");
                }

                using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, m_domainName))
                {
                    if (pc == null)
                    {
                        throw new Exception("Can't create PrincipalContext for this domain.");
                    }
                    if (isGroup)
                    {
                        using (GroupPrincipal groupPrinc = GroupPrincipal.FindByIdentity(pc, IdentityType.SamAccountName, userNameAlone))
                        {
                            if (groupPrinc != null)
                            {
                                try
                                {
                                    // The following line is slow.
                                    using (PrincipalSearchResult<Principal> memberships = groupPrinc.GetMembers())
                                    {
                                        foreach (Principal p in memberships)
                                        {
                                            if (p != null)
                                            {
                                                try
                                                {
                                                    userGroupList.Add(String.Format(@"{0}\{1}", m_domainName, p.Name.ToLower()));
                                                }
                                                finally
                                                {
                                                    p.Dispose();
                                                }
                                            }
                                        }
                                    }
                                }
                                catch (PrincipalOperationException pex)
                                {
                                    Utility.UtilityLog.EventLogException("Sales Commission", "(" + pex.ErrorCode + ") GetUserGroupsFromActiveDirectory exception for " + m_domainName + @"\" + userNameAlone + ".", pex, EventLogEntryType.Warning);
                                }
                                catch (Exception ex)
                                {
                                    Utility.UtilityLog.EventLogException("Sales Commission", "GetUserGroupsFromActiveDirectory exception for " + m_domainName + @"\" + userNameAlone + ".", ex, EventLogEntryType.Warning);
                                }
                            }
                        }
                    }
                    else
                    {
                        using (UserPrincipal userPrinc = UserPrincipal.FindByIdentity(pc, IdentityType.SamAccountName, userNameAlone))
                        {
                            if (userPrinc == null)
                            {
                                throw new Exception("Can't find account identity for [" + userNameAlone + "] in domain [" + m_domainName + "].");
                            }
                            // The following line is slow.
                            bool error1301 = false;
                            using (PrincipalSearchResult<Principal> memberships = userPrinc.GetAuthorizationGroups())
                            {
                                if (memberships == null)
                                {
                                    throw new Exception("Can't determine group memberships.");
                                }

                                using (var iterGroup = memberships.GetEnumerator())
                                {
                                    bool complained = false;
                                    while (iterGroup.MoveNext())
                                    {
                                        string sid = string.Empty;
                                        try
                                        {
                                            using (Principal p = iterGroup.Current)
                                            {
                                                sid = p.Sid.ToString();

                                                if (!m_integritylevelSIDs.Contains(sid))
                                                {
                                                    if (p.Name == null)
                                                    {
                                                        throw new Exception("There is no name attribute for group with SID " + sid);
                                                    }

                                                    userGroupList.Add(String.Format(@"{0}\{1}", m_domainName, p.Name.ToLower()));
                                                }
                                            }
                                        }
                                        catch (NoMatchingPrincipalException)
                                        {
                                            continue;
                                        }
                                        catch (PrincipalOperationException pex)
                                        {
                                            if (!complained)
                                            {
                                                if (string.IsNullOrWhiteSpace(sid))
                                                {
                                                    Utility.UtilityLog.EventLogException("Sales Commission", "(" + pex.ErrorCode + ") GetUserGroupsFromActiveDirectory exception for " + m_domainName + @"\" + userNameAlone + ".", pex, EventLogEntryType.Warning);
                                                }
                                                else
                                                {
                                                    Utility.UtilityLog.EventLogException("Sales Commission", "(" + pex.ErrorCode + ") GetUserGroupsFromActiveDirectory exception on SID " + sid + " for " + m_domainName + @"\" + userNameAlone + ".", pex, EventLogEntryType.Warning);
                                                }
                                                complained = true;
                                                if (pex.ErrorCode == 1301)
                                                {
                                                    error1301 = true;
                                                    break; // see Microsoft KB 2830145
                                                }
                                            }
                                            continue;
                                        }
                                        catch (Exception ex)
                                        {
                                            if (!complained)
                                            {
                                                if (string.IsNullOrWhiteSpace(sid))
                                                {
                                                    Utility.UtilityLog.EventLogException("Sales Commission", "GetUserGroupsFromActiveDirectory exception for " + m_domainName + @"\" + userNameAlone + ".", ex, EventLogEntryType.Warning);
                                                }
                                                else
                                                {
                                                    Utility.UtilityLog.EventLogException("Sales Commission", "GetUserGroupsFromActiveDirectory exception on SID " + sid + " for " + m_domainName + @"\" + userNameAlone + ".", ex, EventLogEntryType.Warning);
                                                }
                                                complained = true;
                                            }
                                            continue;
                                        }
                                    }
                                }

                                if (error1301) // try again using limited API call
                                {
                                    userGroupList = new List<string>();
                                    using (PrincipalSearchResult<Principal> memberships2 = userPrinc.GetGroups())
                                    {
                                        if (memberships2 == null)
                                        {
                                            throw new Exception("Can't determine group memberships.");
                                        }

                                        using (var iterGroup = memberships2.GetEnumerator())
                                        {
                                            bool complained = false;
                                            while (iterGroup.MoveNext())
                                            {
                                                string sid = string.Empty;
                                                try
                                                {
                                                    using (Principal p = iterGroup.Current)
                                                    {
                                                        sid = p.Sid.ToString();

                                                        if (!m_integritylevelSIDs.Contains(sid))
                                                        {
                                                            if (p.Name == null)
                                                            {
                                                                throw new Exception("There is no name attribute for group with SID " + sid);
                                                            }

                                                            userGroupList.Add(String.Format(@"{0}\{1}", m_domainName, p.Name.ToLower()));
                                                        }
                                                    }
                                                }
                                                catch (NoMatchingPrincipalException)
                                                {
                                                    continue;
                                                }
                                                catch (PrincipalOperationException pex)
                                                {
                                                    if (!complained)
                                                    {
                                                        if (string.IsNullOrWhiteSpace(sid))
                                                        {
                                                            Utility.UtilityLog.EventLogException("Sales Commission", "(" + pex.ErrorCode + ") GetUserGroupsFromActiveDirectory exception for " + m_domainName + @"\" + userNameAlone + ".", pex, EventLogEntryType.Warning);
                                                        }
                                                        else
                                                        {
                                                            Utility.UtilityLog.EventLogException("Sales Commission", "(" + pex.ErrorCode + ") GetUserGroupsFromActiveDirectory exception on SID " + sid + " for " + m_domainName + @"\" + userNameAlone + ".", pex, EventLogEntryType.Warning);
                                                        }
                                                        complained = true;
                                                    }
                                                    continue;
                                                }
                                                catch (Exception ex)
                                                {
                                                    if (!complained)
                                                    {
                                                        if (string.IsNullOrWhiteSpace(sid))
                                                        {
                                                            Utility.UtilityLog.EventLogException("Sales Commission", "GetUserGroupsFromActiveDirectory exception for " + m_domainName + @"\" + userNameAlone + ".", ex, EventLogEntryType.Warning);
                                                        }
                                                        else
                                                        {
                                                            Utility.UtilityLog.EventLogException("Sales Commission", "GetUserGroupsFromActiveDirectory exception on SID " + sid + " for " + m_domainName + @"\" + userNameAlone + ".", ex, EventLogEntryType.Warning);
                                                        }
                                                        complained = true;
                                                    }
                                                    continue;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.UtilityLog.EventLogException("Sales Commission", "GetUserGroupsFromActiveDirectory exception for " + userName + ".", ex);
            }

            userGroupList.Sort();

            try
            {
                CacheItemPolicy policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = DateTimeOffset.UtcNow.AddMilliseconds(60000);
                MemoryCache.Default.Add(cacheKey, userGroupList, policy);
            }
            catch
            {
            }

            return userGroupList;
        }

        /// <summary>
        /// Get the list of authorization groups to which a user belongs from the local machine.
        /// 
        /// Information is cached for 60 seconds.
        /// </summary>
        /// <param name="userName">The account name to search.</param>
        /// <returns>A sorted list of the authorization groups to which the user belongs.</returns>
        List<string> GetUserGroupsFromWorkGroup(string userName, bool isGroup = false)
        {
            List<string> userGroupList = null;
            string cacheKey = userName + "GetUserGroupsFromWorkGroup";
            try
            {
                userGroupList = (List<string>)MemoryCache.Default[cacheKey];
                if (userGroupList != null)
                {
                    return userGroupList;
                }
            }
            catch
            {
            }

            userGroupList = new List<string>();

            try
            {
                string userNameAlone = GetMemberName(userName);

                using (PrincipalContext pc = new PrincipalContext(ContextType.Machine))
                {
                    if (isGroup)
                    {
                        using (GroupPrincipal groupPrinc = GroupPrincipal.FindByIdentity(pc, IdentityType.SamAccountName, userNameAlone))
                        {
                            if (groupPrinc != null)
                            {
                                try
                                {
                                    // The following line is slow.
                                    using (PrincipalSearchResult<Principal> memberships = groupPrinc.GetMembers())
                                    {
                                        foreach (Principal p in memberships)
                                        {
                                            if (p != null)
                                            {
                                                try
                                                {
                                                    userGroupList.Add(String.Format(@"{0}\{1}", m_domainName, p.Name.ToLower()));
                                                }
                                                finally
                                                {
                                                    p.Dispose();
                                                }
                                            }
                                        }
                                    }
                                }
                                catch (PrincipalOperationException pex)
                                {
                                    Utility.UtilityLog.EventLogException("Sales Commission", "(" + pex.ErrorCode + ") GetUserGroupsFromActiveDirectory exception for " + m_domainName + @"\" + userName + ".", pex, EventLogEntryType.Warning);
                                }
                                catch (Exception ex)
                                {
                                    Utility.UtilityLog.EventLogException("Sales Commission", "GetUserGroupsFromActiveDirectory exception for " + m_domainName + @"\" + userName + ".", ex, EventLogEntryType.Warning);
                                }
                            }
                        }
                    }
                    else
                    {
                        using (UserPrincipal userPrinc = UserPrincipal.FindByIdentity(pc, IdentityType.SamAccountName, userNameAlone))
                        {
                            // The following line is slow.
                            if (userPrinc != null)
                            {
                                using (PrincipalSearchResult<Principal> memberships = userPrinc.GetAuthorizationGroups())
                                {
                                    using (var iterGroup = memberships.GetEnumerator())
                                    {
                                        bool complained = false;
                                        while (iterGroup.MoveNext())
                                        {
                                            try
                                            {
                                                using (Principal p = iterGroup.Current)
                                                {
                                                    userGroupList.Add(String.Format(@"{0}\{1}", m_domainName, p.Name.ToLower()));
                                                }
                                            }
                                            catch (NoMatchingPrincipalException)
                                            {
                                                continue;
                                            }
                                            catch (Exception ex)
                                            {
                                                if (!complained)
                                                {
                                                    Utility.UtilityLog.EventLogException("Sales Commission", "GetUserGroupsFromWorkGroup exception for " + m_domainName + @"\" + userName + ".", ex, EventLogEntryType.Warning);
                                                    complained = true;
                                                }
                                                continue;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.UtilityLog.EventLogException("Sales Commission", "GetUserGroupsFromWorkGroup exception attempting to locate user name " + userName + ".", ex);
            }

            userGroupList.Sort();

            try
            {
                CacheItemPolicy policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = DateTimeOffset.UtcNow.AddMilliseconds(60000);
                MemoryCache.Default.Add(cacheKey, userGroupList, policy);
            }
            catch
            {
            }

            return userGroupList;
        }

        /// <summary>
        /// User or Group whether is valid or invalid
        /// </summary>
        /// <param name="userOrGroupName"></param>
        /// <returns></returns>
        bool IsValidIdentity(string userOrGroupName)
        {
            bool isValidIdentity = false;
            try
            {
                if (userOrGroupName != null)
                {
                    NTAccount ntAccount = new NTAccount(userOrGroupName);
                    SecurityIdentifier securityIdentifier = (SecurityIdentifier)ntAccount.Translate(typeof(SecurityIdentifier));
                    isValidIdentity = true;
                }
            }
            catch
            {
                isValidIdentity = false;
            }
            return isValidIdentity;
        }

        /// <summary>
        /// Check whether it is LDAP or workgroup
        /// </summary>
        /// <returns></returns>
        bool CheckLdap()
        {
            bool isldap = true;
            try
            {
                System.DirectoryServices.ActiveDirectory.Domain.GetComputerDomain(); // throws if not in a domain.
                isldap = true;
            }
            catch
            {
                isldap = false;
            }

            return isldap;
        }

        public static string GetMemberName(string memberDetails)
        {
            int memberTypeSeperatorIndex = memberDetails.IndexOf("[");
            int memberNameAndDomainSeperatorIndex = memberDetails.IndexOf("\\");


            string memberName = null;
            memberName = memberTypeSeperatorIndex != -1 ? memberDetails.Substring(0, memberTypeSeperatorIndex).Trim() : memberDetails.Trim();
            if (memberNameAndDomainSeperatorIndex != -1)
            {
                memberName = memberName.Substring(memberNameAndDomainSeperatorIndex + 1).Trim();
            }
            else
            {
                memberNameAndDomainSeperatorIndex = memberDetails.IndexOf("@");
                memberName = memberNameAndDomainSeperatorIndex != -1 ? memberName.Substring(0, memberNameAndDomainSeperatorIndex).Trim() : memberName;
            }

            return memberName;
        }

        string ConstructMemberInfo(string memberName, string domainName, string memberType)
        {
            StringBuilder memberDetails = new StringBuilder();
            if (!string.IsNullOrEmpty(domainName))
            {
                memberDetails.Append(domainName);
                memberDetails.Append('\\');
            }

            if (!string.IsNullOrEmpty(memberName))
            {
                memberDetails.Append(memberName);
            }

            //if (!string.IsNullOrEmpty(memberType))
            //{
            //    memberDetails.Append(" [");
            //    memberDetails.Append(memberType);
            //    memberDetails.Append(']');
            //}

            return memberDetails.ToString();
        }

        #endregion

    }

}
