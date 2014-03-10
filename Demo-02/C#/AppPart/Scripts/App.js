var user, lastName, firstName, title, phone, email, userPic, sip, fullName, renderHtml, userPicUrl;
var aboutMe, department, interests, skills, memberships, responsibilities, schools, path, userData;

$(document).ready(function () {
    SP.SOD.executeFunc('sp.js', 'SP.ClientContext', sharepointready());
});

function sharepointready() {
    loadPeoplePicker();
    partProperties();
}

//Load the people picker
function loadPeoplePicker(peoplePickerElementId) {
    window.EnsurePeoplePickerRefinementInit = function (peoplePickerElementId) {
        var schema = new Array();
        schema["PrincipalAccountType"] = "User";
        schema["AllowMultipleValues"] = false;
        schema["Width"] = 300;
        schema["OnUserResolvedClientScript"] = function () {
            var pickerObj = SPClientPeoplePicker.SPClientPeoplePickerDict.peoplePicker_TopSpan;
            var users = pickerObj.GetAllUserInfo();
            var userInfo = '';
            person = users[0];

            if (person != null) {     
                for (var userProperty in person)
                    userInfo += '<div>' + userProperty + ': </div><div>' + person[userProperty] + '</div><br>';

                searchPerson(person.Description);
            }
        };

        SP.SOD.executeFunc("clienttemplates.js", "SPClientTemplates", function () {
            SP.SOD.executeFunc("clientforms.js", "SPClientPeoplePicker_InitStandaloneControlWrapper", function () {
                SPClientPeoplePicker_InitStandaloneControlWrapper("peoplePicker", null, schema);
            });
        });
    }
    EnsurePeoplePickerRefinementInit("peoplePicker");
}

//Use REST Search API to query for user properties
function searchPerson(description) {
    var spAppWebUrl = decodeURIComponent(getQueryStringParameter('SPAppWebUrl'));
    var queryUrl = spAppWebUrl + "/_api/search/query?querytext='WorkEmail:" + description
                            + "'&sourceid='B09A7990-05EA-4AF9-81EF-EDFAB16C4E31'"
                            + "&selectproperties='PreferredName,Path,WorkEmail,PictureURL,JobTitle,Department,WorkPhone,WorkEmail,AboutMe,Office,Manager,Interests,Skills,Memberships,Responsibilities,PastProjects,Schools'";

    $.ajax({ url: queryUrl, method: "GET", headers: { "Accept": "application/json; odata=verbose" }, success: onQuerySuccess, error: onQueryError });
}

//Search query success
function onQuerySuccess(data) {
    var results = data.d.query.PrimaryQueryResult.RelevantResults.Table.Rows.results;
    userData = '';

    //Parse Results
    $.each(results, function () {
        $.each(this.Cells.results, function () {
            if (this.Key == "PreferredName")
                fullName = this.Value;
            if (this.Key == "WorkEmail")
                email = this.Value;
            if (this.Key == "JobTitle")
                title = this.Value;
            if (this.Key == "Department")
                department = this.Value;
            if (this.Key == "WorkPhone")
                phone = this.Value;
            if (this.Key == "PictureURL")
                userPic = this.Value;
            if (this.Key == "Path")
                path = this.Value;
            if (this.Key == "AboutMe" || this.Key == "Skills" || this.Key == "Office" || this.Key == "PastProjects" || this.Key == "Schools" || this.Key == "Interests" || this.Key == "Memberships" || this.Key == "Responsibilities") {
                userData += '<div><span class="ms-soften" style="position: relative;">' + this.Key + ': </span>';                
                if (this.Value != null) {
                    userData += this.Value.replace(/;/g, " | ");
                }
                userData += '</div><br>';
            }
        });

        var userPic = userPic.replace("MThumb", "LThumb");

        $("#basicInfo").show();
        $("#pic").html('<img src="' + userPic.replace("MThumb", "LThumb") + '" alt=' + fullName + '" width=92 height=92 />');
        $("#name").html('<a href="' + path + '">' + fullName + '</a>');
        $("#email").html('<a href=mailto:>' + email + '</a>');
        $("#title").html(title);
        $("#department").html(department);
        $("#phone").html(phone);
        $("#detailInfo").html(userData);

        partResize();
    });
}

//Search query error
function onQueryError(error) {
    $("#detailInfo").append(error.statusText)
}

//Read app part properties
function partProperties() {
    var editmode = getQueryStringParameter('editmode');
    var includeDetails = getQueryStringParameter('boolProp');

    if (editmode == 1) {
        document.getElementById("editmodehdr").style.display = "inline";
        document.getElementById("content").style.display = "none";
    }
    else if (includeDetails == "true") {
        $('#detailInfo').show();

        document.getElementById("editmodehdr").style.display = "none";
        document.getElementById("content").style.display = "inline";

        partResize();
    }
}

//Dynamically resize app part based on content
function partResize() {
    var senderId = getQueryStringParameter('SenderId');
    var width = '330';
    var target = parent.postMessage ? parent : (parent.document.postMessage ? parent.document : undefined);
    target.postMessage('<message senderId=' + senderId + '>resize(' + ($(document).width()) + ',' + ($(document).height()) + ')</message>', '*');
}

//Get a parameter value by a specific key 
function getQueryStringParameter(urlParameterKey) {
    var params = document.URL.split('?')[1].split('&');
    var strParams = '';
    for (var i = 0; i < params.length; i = i + 1) {
        var singleParam = params[i].split('=');
        if (singleParam[0] == urlParameterKey) {
            var param = singleParam[1].replace('#', "");
            return decodeURIComponent(param);
        }
    }
}
