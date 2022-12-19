@ModelType IEnumerable(Of ProntoEntries.Entry)

@Code
    ViewData("Title") = "Entries"
End Code

<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>Index</title>
</head>
<body>
    <div class="orgcontainer create">
        <div class="titlediv">
            <h2>Entered Events</h2>
            @*@Html.ActionLink("Create New", "Create")

                @Html.ActionLink("Change Event", "Index", "RaceEvents")*@
        </div>
        <hr />
        @If Model.Count > 0 Then
            @<Table Class="table">
                <tr>
                    <th>
                        @Html.DisplayNameFor(Function(model) model.ParticipantID)
                    </th>
                    <th>
                        @Html.DisplayNameFor(Function(model) model.RaceID)
                    </th>
                    <th>
                        @Html.DisplayNameFor(Function(model) model.DivisionID)
                    </th>
                    <th>
                        @Html.DisplayNameFor(Function(model) model.Amount)
                    </th>
                    <th>
                        @Html.DisplayNameFor(Function(model) model.Status)
                    </th>
                    @*<th style="text-align: center">
                            Additional Items
                        </th>*@
                </tr>
                <tr>
                    <template>
                        <hr />
                    </template>
                </tr>


                @For Each item In Model
                    @<tr style="border: 15px solid white">
                        <td>
                            @Html.Action("Get_ParticipantName", New With {.Id = item.ParticipantID})
                        </td>
                        <td>
                            @Html.Action("Get_RaceName", New With {.Id = item.RaceID})
                        </td>
                        <td>
                            @Html.Action("Get_DivisionName", New With {.Id = item.DivisionID})
                        </td>
                        <td>
                            R
                            @Html.DisplayFor(Function(modelItem) item.Amount)
                        </td>
                        <td>
                            @Html.DisplayFor(Function(modelItem) item.Status)
                        </td>
                        @*<td>
                                @Html.Action("ViewAddOns", "AddonOptions", New With {.Id = item.PaymentReference, .ParticipantID = item.ParticipantID})

                            </td>*@
                        <td class="PrintConfirmation">

                            @Html.ActionLink("Print Confirmation", "GenerateTicket", New With {.id = item.EntryID})



                            @*  @Html.ActionLink("Details", "Details", New With {.id = item.EntryID}) |
                                @Html.ActionLink("Delete", "Delete", New With {.id = item.EntryID})*@
                        </td>
                    </tr>



                Next

            </Table>
        Else
            @<div style="text-align:center">
                <p>No Upcoming Entries Booked</p>
            </div>
        End If

    </div>
</body>
</html>
