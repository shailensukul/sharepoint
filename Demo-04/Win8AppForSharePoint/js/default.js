// For an introduction to the Blank template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkId=232509
(function () {
    "use strict";

    var app = WinJS.Application;
    var activation = Windows.ApplicationModel.Activation;
    var localSettings = Windows.Storage.ApplicationData.current.localSettings;
    var containerName = "cookieContainer1";
    WinJS.strictProcessing();

    app.onactivated = function (args) {
        if (args.detail.kind === activation.ActivationKind.launch) {  
                args.setPromise(WinJS.UI.processAll().then(function () {
                    if (isAuthenticated())
                     return WinJS.Navigation.navigate("/pages/Search/search.html");
                    else
                    return WinJS.Navigation.navigate("/pages/Login/login.html");
                }));
           
        }
    };
    WinJS.Navigation.addEventListener("navigated", function (eventObject) {
        var url = eventObject.detail.location;
        var host = document.getElementById("contentHost");
        WinJS.Utilities.empty(host);
        eventObject.detail.setPromise(WinJS.UI.Pages.render(url, host, eventObject.detail.state).then(function () {
            WinJS.Application.sessionState.lastUrl = url;
        }));
    });
    function isAuthenticated() {
        if (localSettings.containers.hasKey(containerName)) {
            return true;
        }
        return false;
    }
    app.start();
})();
