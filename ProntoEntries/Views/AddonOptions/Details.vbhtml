@ModelType ProntoEntries.AddonOption
@Code
    ViewData("Title") = "Details"
End Code

<h2>Details</h2>

<div>
    <h4>AddonOption</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.ItemID)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.ItemID)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Size)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Size)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Amount)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Amount)
        </dd>

    </dl>
</div>
<p>
    @Html.ActionLink("Edit", "Edit", New With { .id = Model.OptionID }) |
    @Html.ActionLink("Back to List", "Index")
</p>
