@ModelType ProntoEntries.Entry

@Code
    Layout = Nothing
End Code

<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>Delete</title>
</head>
<body>
    <h3>Are you sure you want to delete this?</h3>
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
        @Using (Html.BeginForm())
            @Html.AntiForgeryToken()
    
            @<div class="form-actions no-color">
                <input type="submit" value="Delete" class="btn btn-default" /> |
                @Html.ActionLink("Back to List", "Index")
            </div>
        End Using
    </div>
</body>
</html>
