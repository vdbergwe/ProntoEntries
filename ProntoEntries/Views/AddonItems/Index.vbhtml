@ModelType IEnumerable(Of ProntoEntries.AddonItem)
@Code
ViewData("Title") = "Index"
End Code

<div class="orgcontainer create">
    <div class="titlediv">
        <h2>Index</h2>
        @Html.ActionLink("Create New", "Create")
    </div>
    <hr />
        
    <div>

    </div>

    <table class="table">
        <tr>
            <th>
                @Html.DisplayNameFor(Function(model) model.RaceID)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.Name)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.Description)
            </th>
        </tr>

        @For Each item In Model
            @<tr>
                <td>
                    @Html.Action("get_RaceName", New With {.Id = item.RaceID})
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.Name)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.Description)
                </td>
                <td>
                    @Html.ActionLink("Add Option", "Create", "AddonOptions", New With {.id = item.ItemID}, Nothing) |
                    @Html.ActionLink("Edit", "Edit", New With {.id = item.ItemID}) |
                    @Html.ActionLink("Delete", "Delete", New With {.id = item.ItemID})
                </td>
            </tr>
            @Html.Action("IndexPartial", "AddonOptions", New With {.Id = item.ItemID})
        Next

    </table>
</div>