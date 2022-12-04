@ModelType IEnumerable(Of ProntoEntries.Participant)

@Code
    ViewData("Title") = "Index"
End Code

<div class="orgcontainer create">
    <div class="titlediv">
        <h2>Search - Entries</h2>
        @If ViewBag.SelectedRace IsNot Nothing Then
            @<div>
                <ul>
                    <li>
                        @Html.ActionLink("Export Race Detail", "ExporttoExcelRaceDetail", New With {.id = ViewBag.SelectedRace})
                    </li>
                    <li>
                        @Html.ActionLink("Export All Participants", "ExporttoExcel", New With {.id = ViewBag.SelectedRace})
                    </li>
                </ul>
            </div>
        End If
    </div>

    <hr />



    <form action="" method="get">
        <div class="DropdownSearches">
            <div Class="DropdownSearches item" , id="eventselect">
                <p>Event Name</p>
                @Html.DropDownList("RaceID", Nothing, htmlAttributes:=New With {.class = "form-controlDDL"}, optionLabel:="Select Event")
            </div>
            <div class="DropdownSearches item">
                <p>Search Value</p>
                <input type="text" name="SearchValue" id="SearchValue" class="form-control" placeholder="@ViewBag.SearchText" />
            </div>
        </div>
    </form>

    <hr />
    @If (Model.Count() > 0) Then
        @<div Class="ReportContent">
            <Table Class="table">
                <tr>
                    <th>
                        @Html.DisplayNameFor(Function(model) model.FirstName)
                    </th>
                    <th>
                        @Html.DisplayNameFor(Function(model) model.LastName)
                    </th>
                    <th>
                        @Html.DisplayNameFor(Function(model) model.IDNumber)
                    </th>
                    <th>
                        @Html.DisplayNameFor(Function(model) model.RaceNumber)
                    </th>
                    <th>
                        @Html.DisplayNameFor(Function(model) model.EmailAddress)
                    </th>
                </tr>

                @For Each item In Model
                    @<tr>
                        <td>
                            @Html.DisplayFor(Function(modelItem) item.FirstName)
                        </td>
                        <td>
                            @Html.DisplayFor(Function(modelItem) item.LastName)
                        </td>
                        <td>
                            @Html.DisplayFor(Function(modelItem) item.IDNumber)
                        </td>
                        <td>
                            @Html.DisplayFor(Function(modelItem) item.RaceNumber)
                        </td>
                        <td>
                            @Html.DisplayFor(Function(modelItem) item.EmailAddress)
                        </td>
                        <td>
                            @Html.ActionLink("Details", "EntryDetail", "Reports", New With {.id = item.ParticipantID, .RaceID = ViewBag.SelectedRace, .SearchValue = ViewBag.SearchText}, Nothing)

                            @*@If (ViewBag.DivisionSelect > 0) Then
                                    @Html.ActionLink("Enter Event", "VerifyEntry", "Entries", New With {.id = item.ParticipantID, .RaceID1 = ViewBag.RaceID, .DivisionID1 = ViewBag.DivisionSelect}, Nothing)
                                    @Html.ActionLink("Add to Cart", "Addtocart", "Entries", New With {.id = item.ParticipantID, .RaceID = ViewBag.RaceID, .DivisionID = ViewBag.DivisionSelect}, Nothing)
                                End If*@
                        </td>
                    </tr>
                Next

            </Table>

        </div>
    Else
        @<p>NO DATA IN SEARCH</p>
    End If



</div>

@*@Section Scripts
        @Scripts.Render("~/bundles/jqueryval")
        <script>
            function runsearch() {
                var RaceID = $("#eventselect option:selected").val();
                var SearchText = $("#SearchValue").val();
                window.location.replace("?RaceID=" + RaceID + "&SearchText=" + SearchText);
            }
        </script>
    End Section*@
