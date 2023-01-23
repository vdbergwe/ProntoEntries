@ModelType IEnumerable(Of ProntoEntries.AddonOption)
@Code
ViewData("Title") = "Index"
End Code


<div class="orgcontainer create">
    <div class="titlediv">
        <h2>@Html.Action("get_RaceName", "AddonItems", New With {.Id = ViewBag.RaceID}) - @Html.Action("get_ItemName", New With {.Id = ViewBag.ItemID})</h2>
        @Html.ActionLink("Create New", "Create")
    </div>
    <hr />

    <table class="table">
        <tr>
            <th>
                @Html.DisplayNameFor(Function(model) model.OptionID)
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
                    @Html.DisplayFor(Function(modelItem) item.OptionID)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.Size)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.Amount)
                </td>
                <td>
                    @Html.ActionLink("Edit", "Edit", New With {.id = item.OptionID}) |
                    @*@Html.ActionLink("Details", "Details", New With {.id = item.OptionID}) |*@
                    @Html.ActionLink("Delete", "Delete", New With {.id = item.OptionID})
                </td>
            </tr>
        Next

    </table>
</div>