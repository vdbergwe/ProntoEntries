﻿@ModelType ProntoEntries.AddonOption
@Code
    ViewData("Title") = "Delete"
End Code

<h2>Delete</h2>

<h3>Are you sure you want to delete this?</h3>
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
    @Using (Html.BeginForm())
        @Html.AntiForgeryToken()

        @<div class="form-actions no-color">
            <input type="submit" value="Delete" class="btn btn-default" /> |
            @Html.ActionLink("Back to List", "Index")
        </div>
    End Using
</div>
