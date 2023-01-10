@ModelType IEnumerable(Of ProntoEntries.Sale)

@Code
    ViewData("Title") = "Entries"
End Code



<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>Edit</title>
</head>
<body>

    @Using (Html.BeginForm())
        @Html.AntiForgeryToken()

        @<div Class="orgcontainer partcreate">
            <div Class="titlediv">
                <h2> Entry - @Html.Action("Get_ParticipantName", New With {.Id = ViewBag.ParticipantID})</h2>
                @Html.ActionLink("Back to List", "Index")
            </div>
            <hr />

            <div Class="ReportContent">
                <Table Class="table">
                    <tr>
                        <th>
                            SaleID
                        </th>
                        <th>
                            Option Name
                        </th>
                        <th>
                            Current Value
                        </th>
                        <th>
                            
                        </th>
                    </tr>

                    @For Each item In Model
                        @<tr>
                            <td>
                                @Html.DisplayFor(Function(modelItem) item.SaleID)
                            </td>
                            <td>
                                @Html.Action("Get_ItemName", New With {.Id = item.ItemID})
                            </td>
                            <td>
                                @Html.Action("Get_ItemChosen", New With {.Id = item.OptionID})
                            </td>
                            <td>
                                @Html.ActionLink("Update", "Update", New With {.id = item.ItemID, .OptionID = item.OptionID, .SaleID = item.SaleID})
                            </td>
                        </tr>
                    Next
                </Table>
            </div>


        </div>

    End Using
</body>
</html>
