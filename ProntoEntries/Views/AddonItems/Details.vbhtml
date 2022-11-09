@ModelType ProntoEntries.AddonItem
@Code
    ViewData("Title") = "Details"
End Code

<h2>Details</h2>

<div>
    <h4>AddonItem</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.RaceID)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.RaceID)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Name)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Name)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Description)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Description)
        </dd>

    </dl>
</div>
<p>
    @Html.ActionLink("Edit", "Edit", New With { .id = Model.ItemID }) |
    @Html.ActionLink("Back to List", "Index")
</p>
