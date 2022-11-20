@ModelType ProntoEntries.RaceEvent
@Code
    ViewData("Title") = ViewBag.RaceName
End Code

<head>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
</head>
<style>
    .backing {
        background: linear-gradient( rgba(0, 0, 0, 0.5), rgba(0, 0, 0, 0.5) ), url(@ViewBag.Background) no-repeat;
        height: 100%;
    }
    .logobox {
        content: url(@ViewBag.OrgImage);
        height: 100%;
        width: auto;
    }
</style>



<div class="orgcontainer create">
    <div class="titlediv">
        <h2>@ViewBag.RaceName</h2>
        <div>
            <ul>
                @If User.Identity.IsAuthenticated And User.IsInRole("Admin") Then
                    @<li>
                        @Html.ActionLink("Edit", "Edit", New With {.id = Model.RaceID})
                    </li>
                End If
                <li>
                    @Html.ActionLink("Back to List", "Index")
                </li>
                <li>
                    @If ViewBag.Closed = True Then
                        @Html.ActionLink("Online Entries Closed", "Index", "RaceEvents")

                    Else
                        @Html.ActionLink("Enter Now", "NewEntry", "Entries", New With {.id = Model.RaceID, .DivisionSelect = 0}, Nothing)
                    End If
                </li>
            </ul>
        </div>
    </div>
    <hr />



    <div class="EventDetail">
        @*<h4>RaceEvent</h4>*@
        <div class="form--el1">
            @Html.DisplayFor(Function(model) model.RaceHtmlPage)
        </div>
        <div class="form--el2">
            <div class="RaceControlPanel">

                <div class="RaceControlBanner">
                    <p>
                        Race Information
                    </p>
                </div>

                <div class="RC--1">
                    <p>
                        @Html.DisplayNameFor(Function(model) model.RaceDescription)
                    </p>
                    <p>
                        @Html.DisplayFor(Function(model) model.RaceDescription)
                    </p>
                </div>

                <div class="RC--1">
                    <p>
                        @Html.DisplayNameFor(Function(model) model.RaceDate)
                    </p>
                    <p>
                        @ViewBag.RaceDate
                    </p>
                </div>

                <div class="RC--1">
                    <p>
                        @Html.DisplayNameFor(Function(model) model.RaceType)
                    </p>
                    <p>
                        @Html.DisplayFor(Function(model) model.RaceType)
                    </p>
                </div>

                @If (Model.DispAddress = True) Then

                    @<div Class="RaceControlBanner Small">
                        <p>
                            Location
                        </p>
                    </div>

                    @<div Class="RC--1">
                        <p>
                            @Html.DisplayNameFor(Function(model) model.Coordinates)
                        </p>
                        <p>
                            @Html.DisplayFor(Function(model) model.Coordinates)
                        </p>
                    </div>

                    @<div Class="RC--1">
                        <p>
                            @Html.DisplayNameFor(Function(model) model.Address)
                        </p>
                        <p>
                            @Html.DisplayFor(Function(model) model.Address)
                        </p>
                    </div>

                    @<div class="RC--1">
                        <p>
                            @Html.DisplayNameFor(Function(model) model.City)
                        </p>
                        <p>
                            @Html.DisplayFor(Function(model) model.City)
                        </p>
                    </div>

                    @<div class="RC--1">
                        <p>
                            @Html.DisplayNameFor(Function(model) model.Province)
                        </p>
                        <p>
                            @Html.DisplayFor(Function(model) model.Province)
                        </p>
                    </div>
                End If

                @If (Model.DispAdmCharge = True) Then
                    @<div Class="RaceControlBanner Small">
                        <p>
                            Admin Amount
                        </p>
                    </div>

                    @<div Class="RC--1">
                        <p>
                            @Html.DisplayNameFor(Function(model) model.AdminCharge)
                        </p>
                        <p>
                            R
                            @Html.DisplayFor(Function(model) model.AdminCharge)
                        </p>
                    </div>
                End If

                <div class="RaceControlBanner Small">
                    <p>
                        Hosted By
                    </p>
                </div>

                <div class="RC--1">
                    <p>
                        @Html.DisplayNameFor(Function(model) model.OrgID)
                    </p>
                    <p>
                        @ViewBag.OrgID
                    </p>
                </div>

                @*<dt>
                        @Html.DisplayNameFor(Function(model) model.Image)
                    </dt>

                    <dd>
                        @Html.DisplayFor(Function(model) model.Image)
                    </dd>*@

                <div Class="RaceControlBanner Small">
                    <p>
                        Categories
                    </p>
                </div>

                <div>
                    @If (Model.DispClasses = True) Then
                        @Html.Action("ViewClasses", "Divisions", New With {.Id = Model.RaceID})
                    End If
                </div>

                <div Class="RaceControlBanner EntryLink">
                    @If ViewBag.Closed = True Then
                        @Html.ActionLink("Online Entries Closed", "Index", "RaceEvents", New With {.class = "btnEntryLink"})
                    Else
                        @Html.ActionLink("Enter Now", "NewEntry", "Entries", New With {.id = Model.RaceID}, New With {.class = "btnEntryLink"})
                    End If
                </div>
            </div>

        </div>

    </div>
    <div Class="RaceControlBanner EntryLink" style="width:100%">
        @If ViewBag.Closed = True Then
            @Html.ActionLink("Online Entries Closed", "Index", "RaceEvents", New With {.class = "btnEntryLink"})
        Else
            @Html.ActionLink("Enter Now", "NewEntry", "Entries", New With {.id = Model.RaceID}, New With {.class = "btnEntryLink"})
        End If
    </div>
</div>


@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")


End Section
