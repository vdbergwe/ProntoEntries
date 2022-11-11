@ModelType IEnumerable(Of ProntoEntries.AddonOption)
@Code
ViewData("Title") = "Index"
End Code

<table>
    <tr>
        <th>
            Item
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Size)
        </th>
    </tr>

@For Each item In Model
    @<tr>
        <td>
            @Html.Action("Get_ItemName", New With {.Id = item.ItemID})
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Size)
        </td>
    </tr>
Next

</table>
