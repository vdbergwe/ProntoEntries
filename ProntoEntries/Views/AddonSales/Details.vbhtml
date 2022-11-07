@ModelType ProntoEntries.AddonSale
@Code
    ViewData("Title") = "Details"
End Code

<h2>Details</h2>

<div>
    <h4>AddonSale</h4>
    <hr />
    <dl class="dl-horizontal">
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

        <dt>
            @Html.DisplayNameFor(Function(model) model.EntryID)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.EntryID)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.RaceID)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.RaceID)
        </dd>

    </dl>
</div>
<p>
    @Html.ActionLink("Edit", "Edit", New With { .id = Model.ItemID }) |
    @Html.ActionLink("Back to List", "Index")
</p>
