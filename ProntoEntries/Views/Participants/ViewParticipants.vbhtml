@ModelType IEnumerable(Of ProntoEntries.Participant)
@Code
    ViewData("Title") = "Participants"
End Code

<div class="orgcontainer">
    <div class="titlediv">
        <h2>Participants</h2>
        @Html.ActionLink("Create New", "Create", New With {.EventID = ViewBag.RaceID, .Distance = ViewBag.DivisionSelect})

    </div>

    <hr />
    <div>
        @If Model.Count() = 0 Then
            @<p>
                No Available Participants. Please select from options below.
            </p>
        Else
            @<Table Class="table" style="margin: 0 10%">
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
                        Age
                    </th>
                        @*<th>
                            @Html.DisplayNameFor(Function(model) model.EmailAddress)
                        </th>*@
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
                            @Html.Action("Get_Age", "Participants", New With {.Id = item.ParticipantID, .RaceID = ViewBag.RaceID})
                        </td>
                            @*<td>
                                @Html.DisplayFor(Function(modelItem) item.EmailAddress)
                            </td>*@
                        @If (ViewBag.DivisionSelect > 0) Then
                            @<td Class="linkbutton">

                                @Html.ActionLink("Enter Event", "VerifyEntry", "Entries", New With {.id = item.ParticipantID, .RaceID1 = ViewBag.RaceID, .DivisionID1 = ViewBag.DivisionSelect}, Nothing)

                                @*@<div Class="button-group">
                                        <div Class="linkbutton">
                                        </div>
                                    </div>*@
                                @*@Html.ActionLink("Enter Event", "VerifyEntry", "Entries", New With {.id = item.ParticipantID, .RaceID1 = ViewBag.RaceID, .DivisionID1 = ViewBag.DivisionSelect}, Nothing)*@


                            </td>
                        End If
                    </tr>
                Next

            </Table>
        End If

        <hr />
        <div class="raceflex">
            <div class="linkbutton">
                @*@Html.ActionLink("Create New1", "Create")*@
                @Html.ActionLink("Create New", "Create", New With {.EventID = ViewBag.RaceID, .Distance = ViewBag.DivisionSelect})

            </div>
            <div class="linkbutton">
                @Html.ActionLink("View Cart", "Cart", "Entries")
            </div>
        </div>

    </div>

</div>
