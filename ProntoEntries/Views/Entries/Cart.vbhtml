@ModelType IEnumerable(Of ProntoEntries.Entry)

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
    <p>
        @*@Html.ActionLink("Add Entry", "NewEntry")*@
    </p>
    <table class="table">
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
            <th></th>
        </tr>

        @For Each item In Model
            @<tr>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.ParticipantID)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.RaceID)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.DivisionID)
                </td>
                <td>
                    R
                    @Html.DisplayFor(Function(modelItem) item.Amount)
                </td>
                <td>
                    @*@Html.ActionLink("Edit", "Edit", New With {.id = item.EntryID}) |
                        @Html.ActionLink("Details", "Details", New With {.id = item.EntryID}) |*@
                    @Html.ActionLink("Remove", "Delete", New With {.id = item.EntryID})
                </td>
            </tr>
        Next


    </table>
    <div>
        Total: R
        @ViewBag.Total
    </div>
</body>
</html>
