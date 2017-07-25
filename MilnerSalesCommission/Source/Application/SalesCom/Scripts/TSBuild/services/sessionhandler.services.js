var mtisalescom;
(function (mtisalescom) {
    'use strict';
    var SessionHandler = (function () {
        function SessionHandler($http) {
            this.$http = $http;
            this.lastActivity = new Date().getTime();
            this.activityTimeout = 15;
            this.GetSessionTimeout();
            this.count = 0;
        }
        SessionHandler.prototype.GetSessionTimeout = function () {
            var parentScope = this;
            parentScope.$http({
                url: preurl('Home/GetSessionTimeout'),
                method: "GET",
                withCredentials: true
            }).then(function (response) {
                parentScope.activityTimeout = response.data;
            });
        };
        SessionHandler.prototype.KeepAlive = function (self) {
            var currentTime = new Date().getTime();
            var timeDiff = currentTime - self.lastActivity;
            var minutesElapsed = Math.floor(timeDiff / 60000);
            if (minutesElapsed >= self.activityTimeout) {
                self.$http({
                    url: preurl('Home/Activity'),
                    method: "GET",
                    withCredentials: true
                }).success(function (r) {
                    self.lastActivity = new Date().getTime();
                    self.count++;
                    setTimeout(self.KeepAlive, self.activityTimeout * 60000, self);
                }).error(function (xhr, status, err) {
                    setTimeout(self.KeepAlive, self.activityTimeout * 60000, self);
                });
            }
            else {
                setTimeout(self.KeepAlive, self.activityTimeout * 60000, self);
            }
        };
        SessionHandler.prototype.SetLastActivity = function (value) {
            this.lastActivity = value;
        };
        SessionHandler.$inject = ['$http'];
        return SessionHandler;
    }());
    mtisalescom.SessionHandler = SessionHandler;
    sessionActivityfactory.$inject = ['$http'];
    function sessionActivityfactory($http) {
        return new SessionHandler($http);
    }
    mtisalescom.sessionActivityfactory = sessionActivityfactory;
    angular
        .module('mtisalescom')
        .factory('mtisalescom.SessionHandler', sessionActivityfactory);
})(mtisalescom || (mtisalescom = {}));
//# sourceMappingURL=E:/Projects/SalesCommission/Source/Application/SalesCommission//Scripts/TSBuild/services/sessionhandler.services.js.map