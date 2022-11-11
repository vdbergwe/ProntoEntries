@ModelType IEnumerable(Of ProntoEntries.AddonOption)
@Code
ViewData("Title") = "Index"
End Code

<table class="table">
    <tr>
        <th>

        </th>
        <th>
            Description
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
        <td></td>
        <td>
            @Html.Action("Get_ItemName", New With {.Id = item.ItemID})
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Size)
        </td>
        <td>
            R
            @Html.DisplayFor(Function(modelItem) item.Amount)
        </td>
        @*<td>
            @Html.ActionLink("Edit", "Edit", New With {.id = item.OptionID }) |
            @Html.ActionLink("Details", "Details", New With {.id = item.OptionID }) |
            @Html.ActionLink("Delete", "Delete", New With {.id = item.OptionID })
        </td>*@
    </tr>
Next

</table>
