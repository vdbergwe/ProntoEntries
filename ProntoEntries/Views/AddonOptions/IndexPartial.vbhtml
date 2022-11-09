@ModelType IEnumerable(Of ProntoEntries.AddonOption)
@Code
ViewData("Title") = "Index"
End Code

<table class="table">
    <tr>
        <th>
            @Html.DisplayNameFor(Function(model) model.ItemID)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Size)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Amount)
        </th>
        <th></th>
    </tr>

@For Each item In Model
    @<tr>
        <td>
            @Html.DisplayFor(Function(modelItem) item.ItemID)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Size)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Amount)
        </td>
        <td>
            @Html.ActionLink("Edit", "Edit", New With {.id = item.OptionID }) |
            @Html.ActionLink("Details", "Details", New With {.id = item.OptionID }) |
            @Html.ActionLink("Delete", "Delete", New With {.id = item.OptionID })
        </td>
    </tr>
Next

</table>
