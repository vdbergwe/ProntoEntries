@ModelType IEnumerable(Of ProntoEntries.Voucher)

@Code
    Layout = Nothing
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
                @Html.DisplayNameFor(Function(model) model.Code)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.Value)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.IssuedBy)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.Pf_Reference)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.M_Reference)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.Date)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.Status)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.UsedBy)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.UsedDate)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.UsedM_Reference)
            </th>
            <th></th>
        </tr>
    
    @For Each item In Model
        @<tr>
            <td>
                @Html.DisplayFor(Function(modelItem) item.Code)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.Value)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.IssuedBy)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.Pf_Reference)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.M_Reference)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.Date)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.Status)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.UsedBy)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.UsedDate)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.UsedM_Reference)
            </td>
            <td>
                @Html.ActionLink("Edit", "Edit", New With {.id = item.VoucherID }) |
                @Html.ActionLink("Details", "Details", New With {.id = item.VoucherID }) |
                @Html.ActionLink("Delete", "Delete", New With {.id = item.VoucherID })
            </td>
        </tr>
    Next
    
    </table>
</body>
</html>
