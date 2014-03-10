// For an introduction to the Grid template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkID=232446
(function () {
    "use strict";

    WinJS.Binding.optimizeBindingReferences = true;

    var app = WinJS.Application;
    var activation = Windows.ApplicationModel.Activation;
    var ui = WinJS.UI;

    app.onactivated = function (args) {
        if (args.detail.kind === activation.ActivationKind.launch) {

            if (launch != undefined &&  launch != "") {
                window.open(launch, '_blank');
                window.focus();
            }

            if (args.detail.previousExecutionState !== activation.ApplicationExecutionState.terminated) {
                // TODO: This application has been newly launched. Initialize
                // your application here.
            } else {
                // TODO: This application has been reactivated from suspension.
                // Restore application state here.
            }
            args.setPromise(WinJS.UI.processAll());
        }
    };

    app.oncheckpoint = function (args) {
        // TODO: This application is about to be suspended. Save any state
        // that needs to persist across suspensions here. You might use the
        // WinJS.Application.sessionState object, which is automatically
        // saved and restored across suspension. If you need to complete an
        // asynchronous operation before your application is suspended, call
        // args.setPromise().
    };

    app.start();

    var launch;

    var pushNotifications = Windows.Networking.PushNotifications;
    function onPushNotification(e) {
        var notificationPayload;

        launch = "";
        switch (e.notificationType) {
            case pushNotifications.PushNotificationType.toast:
                launch = e.toastNotification.content.documentElement.getAttribute("launch");              
                notificationPayload = e.toastNotification.content.getXml();
                break;

            case pushNotifications.PushNotificationType.tile:
                notificationPayload = e.tileNotification.content.getXml();
                break;

            case pushNotifications.PushNotificationType.badge:
                notificationPayload = e.badgeNotification.content.getXml();
                break;

            case pushNotifications.PushNotificationType.raw:
                notificationPayload = e.rawNotification.content;
                break;
        }       

       // e.cancel = true;
        e.cancel = false;
    }

    ui.Pages.define("default.html", {
        ready: function (element, options) {
            WinJS.Resources.processAll();

            document.querySelector("#btnPost").addEventListener("click", function () {
                var channel,
                    pushNotifications = Windows.Networking.PushNotifications,
                    channelOperation = pushNotifications.PushNotificationChannelManager.createPushNotificationChannelForApplicationAsync(),
                    errorMsg = "",
                    txtAzureWebsiteUrl = document.querySelector("#txtAzureWebsiteUrl");
               

                if (txtAzureWebsiteUrl && txtAzureWebsiteUrl.value.trim().length > 0) {
                    channelOperation.then(
                        function (newChannel) {
                            channel = newChannel;
                            channel.addEventListener("pushnotificationreceived", onPushNotification, false);
                        },
                        function (error) {
                            errorMsg = error.message;
                        }
                    ).then(
                    function () {
                        if (errorMsg.length > 0) return;
                        var siteUri = txtAzureWebsiteUrl.value.trim() + "/Pages/Default.aspx?ChannelUrl=" + encodeURI(channel.uri);
                        return WinJS.xhr({
                            url: siteUri,
                            type: "GET"
                        });
                    }).then(
                        function complete(result) {
                            document.querySelector("#output").textContent = "Channel Url Sent Successfully";
                        },
                        function error(error) {
                            errorMsg = error.message;
                        }
                    ).done(function () {
                        if (errorMsg.length > 0) {
                            document.querySelector("#output").textContent = errorMsg;
                        }
                    });
                }
            });

        }
    });

})();
