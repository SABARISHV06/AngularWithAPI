// Copyright 2016-2017, Milner Technologies, Inc.
//
// This document contains data and information proprietary to
// Milner Technologies, Inc.  This data shall not be disclosed,
// disseminated, reproduced or otherwise used outside of the
// facilities of Milner Technologies, Inc., without the express
// written consent of an officer of the corporation.
//


using GlobalConfig;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;


namespace Security
{

    /// <summary>
    /// Sending email message using SMTP Server
    /// </summary>
    public class EmailMessage : IDisposable
    {
        #region ReadOnly Properties
        /// <summary>
        /// Time out
        /// </summary>
        private readonly int m_TimeOut = 180000;
        /// <summary>
        /// Assign SMTP server host name
        /// </summary>
        private readonly string m_Host;
        /// <summary>
        /// Assign SMTP port number
        /// </summary>
        private readonly int m_Port;
        /// <summary>
        /// Assign SMTP user name
        /// </summary>
        private readonly string m_User;
        /// <summary>
        /// Assign SMTP password
        /// </summary>
        private readonly string m_Pass;
        /// <summary>
        /// To enable SSL
        /// </summary>
        private readonly bool m_SSl;
        /// <summary>
        /// Use Default credentials
        /// </summary>
        private readonly bool m_IsDefaultCredentials;
        #endregion

        #region Properties
        /// <summary>
        /// Assign Attachment to send an email
        /// </summary>
        private Attachment m_Attachments = null;
        /// <summary>
        /// This object has been disposed.
        /// </summary>
        private bool m_Disposed = false;
        /// <summary>
        /// Assign From Email Address
        /// </summary>
        public string Sender { get; set; }
        /// <summary>
        /// Assign To Email Address
        /// </summary>
        public string RecipientTo { get; set; }
        /// <summary>
        /// Assign CC Email Address
        /// </summary>
        public string RecipientCC { get; set; }
        /// <summary>
        /// Assign Subject
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// Assign Body message
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// Assign attachment file
        /// </summary>
        public string AttachmentFile { get; set; }
        /// <summary>
        /// Assign is body html
        /// </summary>
        public bool IsBodyHtml { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public EmailMessage()
        {
            GlobalConfiguration settings = new GlobalConfiguration();
            settings.LoadGlobalSettings();
            //MailServer - Represents the SMTP Server
            m_Host = settings.EmailHost;
            //Port- Represents the port number
            m_Port = settings.EmailPort;
            m_TimeOut = settings.EmailTimeout;
            m_IsDefaultCredentials = Convert.ToBoolean(settings.EmailUseDefaultCredentials);
            m_User = settings.EmailUserName;
            m_Pass = settings.EmailPassword;
            m_SSl = settings.EmailSSL;

            Sender = settings.EmailSender;
        }
        #endregion
        
        #region Methods
        /// <summary>
        /// Send an email
        /// </summary>
        public bool Send(EmailMessage mail)
        {
            try
            {
                using (MailMessage message = new MailMessage(Sender, mail.RecipientTo, mail.Subject, mail.Body) { IsBodyHtml = true })
                {
                    if (mail.RecipientCC != null)
                    {
                        message.Bcc.Add(mail.RecipientCC);
                    }

                    using (SmtpClient smtp = new SmtpClient(m_Host, m_Port))
                    {
                        if (!String.IsNullOrEmpty(mail.AttachmentFile))
                        {
                            if (File.Exists(mail.AttachmentFile))
                            {
                                m_Attachments = new Attachment(mail.AttachmentFile);
                                message.Attachments.Add(m_Attachments);
                            }
                        }

                        if (m_IsDefaultCredentials)
                        {
                            smtp.Timeout = m_TimeOut * 60 * 1000;
                            smtp.UseDefaultCredentials = false; // m_IsDefaultCredentials;
                            smtp.Credentials = new NetworkCredential(m_User, m_Pass);
                            smtp.EnableSsl = m_SSl;
                        }

                        smtp.Send(message);
                        return true;
                    }
                }
            }
            catch (SmtpException smtpExp)
            {
                string msg = smtpExp.Message.ToString();
                throw smtpExp;
            }
            //return false;
        }
        #endregion

        #region IDisposable Pattern

        /// <summary>
        /// Implement IDisposable.
        /// Do not make this method virtual. 
        /// A derived class should not be able to override this method. 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            // This object will be cleaned up by the Dispose method. 
            // Therefore, you should call GC.SupressFinalize to 
            // take this object off the finalization queue 
            // and prevent finalization code for this object 
            // from executing a second time.

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Implement idisposable. 
        /// </summary>
        /// 
        /// <param name="disposing">If disposing equals true, the method has been called directly 
        /// or indirectly by a user's code. Managed and unmanaged resources 
        /// can be disposed. 
        /// If disposing equals false, the method has been called by the 
        /// runtime from inside the finalizer and you should not reference 
        /// other objects. Only unmanaged resources can be disposed.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.

            if (!this.m_Disposed)
            {
                // If disposing equals true, dispose all managed 
                // and unmanaged resources.

                if (disposing)
                {
                    // Dispose managed resources here.
                    if (m_Attachments != null)
                    {
                        m_Attachments.Dispose();
                        m_Attachments = null;
                    }
                }

                // Call the appropriate methods to clean up 
                // unmanaged resources here.  If disposing is false, only the following code is executed.

                // Note disposing has been done.

                m_Disposed = true;
            }
        }

        #endregion
    }
}
