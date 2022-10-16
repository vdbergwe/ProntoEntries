﻿@ModelType ProntoEntries.Entry

@Code
    Layout = Nothing
End Code

<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>Details</title>
</head>
<body>
    <div>
        <h4>Entry</h4>
        <hr />
        <dl class="dl-horizontal">
            <dt>
                @Html.DisplayNameFor(Function(model) model.ParticipantID)
            </dt>
    
            <dd>
                @Html.DisplayFor(Function(model) model.ParticipantID)
            </dd>
    
            <dt>
                @Html.DisplayNameFor(Function(model) model.RaceID)
            </dt>
    
            <dd>
                @Html.DisplayFor(Function(model) model.RaceID)
            </dd>
    
            <dt>
                @Html.DisplayNameFor(Function(model) model.DivisionID)
            </dt>
    
            <dd>
                @Html.DisplayFor(Function(model) model.DivisionID)
            </dd>
    
            <dt>
                @Html.DisplayNameFor(Function(model) model.Amount)
            </dt>
    
            <dd>
                @Html.DisplayFor(Function(model) model.Amount)
            </dd>
    
            <dt>
                @Html.DisplayNameFor(Function(model) model.Status)
            </dt>
    
            <dd>
                @Html.DisplayFor(Function(model) model.Status)
            </dd>
    
            <dt>
                @Html.DisplayNameFor(Function(model) model.PaymentReference)
            </dt>
    
            <dd>
                @Html.DisplayFor(Function(model) model.PaymentReference)
            </dd>
    
            <dt>
                @Html.DisplayNameFor(Function(model) model.DistanceChange)
            </dt>
    
            <dd>
                @Html.DisplayFor(Function(model) model.DistanceChange)
            </dd>
    
            <dt>
                @Html.DisplayNameFor(Function(model) model.ChangePaymentRef)
            </dt>
    
            <dd>
                @Html.DisplayFor(Function(model) model.ChangePaymentRef)
            </dd>
    
            <dt>
                @Html.DisplayNameFor(Function(model) model.TransferID)
            </dt>
    
            <dd>
                @Html.DisplayFor(Function(model) model.TransferID)
            </dd>
    
            <dt>
                @Html.DisplayNameFor(Function(model) model.Result)
            </dt>
    
            <dd>
                @Html.DisplayFor(Function(model) model.Result)
            </dd>
    
        </dl>
    </div>
    <p>
        @Html.ActionLink("Edit", "Edit", New With { .id = Model.EntryID }) |
        @Html.ActionLink("Back to List", "Index")
    </p>
</body>
</html>
