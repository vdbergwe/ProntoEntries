@ModelType ProntoEntries.Organiser
@Code
    ViewData("Title") = "Delete"
End Code

<h2>Delete</h2>

<h3>Are you sure you want to delete this?</h3>
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
    @Using (Html.BeginForm())
        @Html.AntiForgeryToken()

        @<div class="form-actions no-color">
            <input type="submit" value="Delete" class="btn btn-default" /> |
            @Html.ActionLink("Back to List", "Index")
        </div>
    End Using
</div>
