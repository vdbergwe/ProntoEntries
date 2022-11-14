@ModelType IEnumerable(Of ProntoEntries.Sale)

@Code
    ViewData("Title") = "Cart"
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
            <h2>Cart</h2>
        </div>
        <hr />



        <Table Class="table">

            @For Each Ditem In ViewBag.UniqueP
                @<tr style="justify-content:center">
                    <td style="color: dodgerblue">
                        @Html.Action("Get_ParticipantName", New With {.Id = (Ditem)})
                    </td>
                </tr>
                @<tr>
                    <th>
                        Event
                    </th>
                    <th>
                        Distance
                    </th>
                    <th>
                        Add-On
                    </th>
                    <th>
                        Amount
                    </th>
                    <th></th>
                </tr>
                @For Each item In Model
                    @If item.ParticipantID = (Ditem) Then
                        @<tr>

                            <td>
                                @Html.Action("Get_RaceName", New With {.Id = item.RaceID})
                            </td>
                            <td>
                                @Html.Action("Get_DivisionDistance", New With {.Id = item.DivisionID})                             
                            </td>
                            <td>
                                @Html.Action("Get_ItemName", "AddonOptions", New With {.Id = item.ItemID})
                            </td>
                            <td>
                                R
                                @Html.Action("Get_CartAmount", New With {.Id = item.DivisionID, .OptionID = item.OptionID})
                            </td>
                            <td>
                                @*@Html.ActionLink("Edit", "Edit", New With {.id = item.EntryID}) |
                                    @Html.ActionLink("Details", "Details", New With {.id = item.EntryID}) |*@
                                @Html.ActionLink("Remove", "Delete", "Sales", New With {.id = item.SaleID}, Nothing)
                            </td>
                        </tr>
                    End If

                Next
            Next


            <tr>
                <td></td>
                <td></td>
                <td></td>
                <td>
                    <hr />
                </td>
                <td>
                    <hr />
                </td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td></td>
                <td>Total :     </td>
                <td>
                    R
                    @ViewBag.Total
                </td>
            </tr>

        </Table>

        <div class="button-cart">
            <div class="linkbutton cart">
                @Html.ActionLink("Payment", "SubmitToPayfast", "Entries")
            </div>

        </div>


    </div>

</body>
</html>

