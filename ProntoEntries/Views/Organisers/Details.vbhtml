@ModelType ProntoEntries.Organiser
@Code
    ViewData("Title") = "Details"
End Code

<h2>Details</h2>

<div>
    <h4>Organiser</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.OrgName)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.OrgName)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.OrgEmail)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.OrgEmail)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.OrgTel)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.OrgTel)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.OrgWebsite)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.OrgWebsite)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.OrgVatNumber)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.OrgVatNumber)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.AdminUserID)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.AdminUserID)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Image)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Image)
        </dd>

    </dl>
</div>
<p>
    @Html.ActionLink("Edit", "Edit", New With { .id = Model.OrgID }) |
    @Html.ActionLink("Back to List", "Index")
</p>
