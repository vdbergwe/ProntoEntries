@ModelType IEnumerable(Of ProntoEntries.Sale)
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
            @Html.DisplayNameFor(Function(model) model.RaceID)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.DivisionID)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.ItemID)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.UserID)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Indemnity)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.TandC)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.ParticipantID)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.M_reference)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Pf_reference)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.OptionID)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Verified)
        </th>
        <th></th>
    </tr>

@For Each item In Model
    @<tr>
        <td>
            @Html.DisplayFor(Function(modelItem) item.RaceID)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.DivisionID)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.ItemID)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.UserID)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Indemnity)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.TandC)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.ParticipantID)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.M_reference)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Pf_reference)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.OptionID)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Verified)
        </td>
        <td>
            @Html.ActionLink("Edit", "Edit", New With {.id = item.SaleID }) |
            @Html.ActionLink("Details", "Details", New With {.id = item.SaleID }) |
            @Html.ActionLink("Delete", "Delete", New With {.id = item.SaleID })
        </td>
    </tr>
Next

</table>
