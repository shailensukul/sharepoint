
(function () {
    "use strict";
    var localSettings = Windows.Storage.ApplicationData.current.localSettings;
    var containerName = "exampleContainer1";
    var searchResults = new Array();
    var page = WinJS.UI.Pages.define("/pages/Search/search.html", {
        ready: function (element, options) {
            document.getElementById("searchBtn").addEventListener("click", getSearchData, false);
        }
    });

    function getSearchData()
    {
        //Note: Only seems to work against sites in the "/sites" path - problem within the DAL/Uri logic

        if (localSettings.containers.hasKey(containerName)) {
            var FedAuth = localSettings.containers.lookup(containerName).values["FedAuth"];
            var RtFa = localSettings.containers.lookup(containerName).values["RtFa"];
            var Url = localSettings.containers.lookup(containerName).values["Url"];
            var Query = document.getElementById("searchBox").value;
            if (Query == "") {           
                //Creating message dialog box 
                var messagedialogpopup = new Windows.UI.Popups.MessageDialog("Input can not be empty!","Error");
                messagedialogpopup.showAsync();
                return;
            }
            var lstView = document.getElementById("searchListView").winControl;
            //clear array items
            searchResults = [];
            var dataList = new WinJS.Binding.List(searchResults);
            lstView.itemDataSource = dataList.dataSource;
            document.querySelector(".progress").style.display = "block";
            var json = JSON.stringify({ "FedAuth": FedAuth, "RtFa": RtFa, "url": Url, "query": Query });
            WinJS.xhr({
                type: "POST",
                url: "http://localhost:2738/Office365ClaimsService.svc/search",
                headers: { "content-type": "application/json; charset=utf-8" },
                data: json,
            }).done(processData, dataError);

        }
    }


    function processData(response) {   
        document.querySelector(".progress").style.display = "none";
      
        var data = JSON.parse(response.response);
        data = JSON.parse(data);
        
        var results = data.d.query.PrimaryQueryResult.RelevantResults.Table.Rows.results;
        for (var i = 0, len = results.length; i < len; i++) {
            var item = results[i].Cells;
            var date = new Date(item.results[8].Value);
            var resultItem = {
                author: item.results[4].Value,
                title: item.results[3].Value,
                date: date.toDateString(),
                url: item.results[6].Value,
                content: item.results[10].Value,
            };
            searchResults.push(resultItem);
        }
        var lstView = document.getElementById("searchListView").winControl;      
        var dataList = new WinJS.Binding.List(searchResults);
        lstView.itemDataSource = dataList.dataSource;

    }
    function dataError(data)
    {
        document.querySelector(".progress").style.display = "none";
        //Creating message dialog box 
        var messagedialogpopup = new Windows.UI.Popups.MessageDialog("An error occurred!", "Error");
        messagedialogpopup.showAsync();
        var d = data;
    }
})();
