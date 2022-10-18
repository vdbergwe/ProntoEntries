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
    <p>
        @Html.ActionLink("Create New", "Create")
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
            <th>
                @Html.DisplayNameFor(Function(model) model.Status)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.PaymentReference)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.DistanceChange)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.ChangePaymentRef)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.TransferID)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.Result)
            </th>
            <th></th>
        </tr>

        @For Each item In Model
            @If (item.Status = "Paid") Then
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
                        @Html.DisplayFor(Function(modelItem) item.Amount)
                    </td>
                    <td>
                        @Html.DisplayFor(Function(modelItem) item.Status)
                    </td>
                    <td>
                        @Html.DisplayFor(Function(modelItem) item.PaymentReference)
                    </td>
                    <td>
                        @Html.DisplayFor(Function(modelItem) item.DistanceChange)
                    </td>
                    <td>
                        @Html.DisplayFor(Function(modelItem) item.ChangePaymentRef)
                    </td>
                    <td>
                        @Html.DisplayFor(Function(modelItem) item.TransferID)
                    </td>
                    <td>
                        @Html.DisplayFor(Function(modelItem) item.Result)
                    </td>
                    <td>
                        @Html.ActionLink("Edit", "Edit", New With {.id = item.EntryID}) |
                        @Html.ActionLink("Details", "Details", New With {.id = item.EntryID}) |
                        @Html.ActionLink("Delete", "Delete", New With {.id = item.EntryID})
                    </td>
                </tr>
            End If

        Next

    </table>
</body>
</html>
