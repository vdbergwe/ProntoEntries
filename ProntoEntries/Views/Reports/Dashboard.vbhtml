@ModelType IEnumerable(Of ProntoEntries.Participant)

@Code
    ViewData("Title") = "Index"
End Code

<div class="orgcontainer create">
    <div class="titlediv">
        <h2>Event Dashboard</h2>
        @If ViewBag.SelectedRace IsNot Nothing Then
            @<div>
                <ul>
                    <li>
                        @Html.ActionLink("Export All Participants", "ExporttoExcel", New With {.id = ViewBag.SelectedRace})
                    </li>
                </ul>
            </div>
        End If
    </div>

    <hr />

    <form action="" method="get" style="text-align:center">
        <div class="DropdownDashboard">
            <div Class="DropdownDashboard item" , id="eventselect">
                <p>Event Name</p>
                @Html.DropDownList("RaceID", Nothing, htmlAttributes:=New With {.class = "form-controlDDL"}, optionLabel:="Select Event")
            </div>
            <input type="submit" name="submit" />
            @*<div class="DropdownSearches item">
                    <p>Search Value</p>
                    <input type="text" name="SearchValue" id="SearchValue" class="form-control" placeholder="@ViewBag.SearchText" />
                </div>*@
        </div>
    </form>

    <hr />

    @If ViewBag.SelectedRace IsNot Nothing Then
        @* MAIN CONTAINER FLEX*@

        @<div Class="dashboardcontainer">

    @* TOTAL ENTRIES PER DIVISION*@

    <div Class="reportitem">
        @Html.Action("Get_EntriesPerDivision", New With {.RaceId = ViewBag.SelectedRace})
    </div>

    @* TOTAL ADD-ONS PER OPTION*@

    <div Class="reportitem">
        @Html.Action("Get_OptionTotals", New With {.RaceId = ViewBag.SelectedRace})
    </div>

    @* FUNDS RECEIVED PER DIVISION*@

    <div Class="reportitem">
        @Html.Action("Get_Financials", New With {.RaceId = ViewBag.SelectedRace})
    </div>

    @* FUNDS RECEIVED PER DIVISION*@

    <div Class="reportitem">
        @Html.Action("Get_FinancialsAddons", New With {.RaceId = ViewBag.SelectedRace})
    </div>

    @* FUNDS RECEIVED PAYFAST*@

    <div Class="reportitem">
        @Html.Action("Get_PayFast", New With {.RaceId = ViewBag.SelectedRace})
    </div>

</div>
    Else
        @<div style="text-align:center">
            <p>Please Select Event</p>
        </div>
    End If

</div>
