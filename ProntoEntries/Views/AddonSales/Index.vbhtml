@ModelType IEnumerable(Of ProntoEntries.AddonSale)
@Code
ViewData("Title") = "Index"
End Code

<h2>Index</h2>

<p>
    @Html.ActionLink("Create New", "Create")
</p>
<table class="table">
    <tr>
        <th>
            @Html.DisplayNameFor(Function(model) model.Name)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Description)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Size)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Amount)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.EntryID)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.RaceID)
        </th>
        <th></th>
    </tr>

@For Each item In Model
    @<tr>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Name)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Description)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Size)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Amount)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.EntryID)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.RaceID)
        </td>
        <td>
            @Html.ActionLink("Edit", "Edit", New With {.id = item.ItemID }) |
            @Html.ActionLink("Details", "Details", New With {.id = item.ItemID }) |
            @Html.ActionLink("Delete", "Delete", New With {.id = item.ItemID })
        </td>
    </tr>
Next

</table>
