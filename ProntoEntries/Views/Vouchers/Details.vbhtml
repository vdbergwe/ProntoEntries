@ModelType ProntoEntries.Voucher

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
        <h4>Voucher</h4>
        <hr />
        <dl class="dl-horizontal">
            <dt>
                @Html.DisplayNameFor(Function(model) model.Code)
            </dt>
    
            <dd>
                @Html.DisplayFor(Function(model) model.Code)
            </dd>
    
            <dt>
                @Html.DisplayNameFor(Function(model) model.Value)
            </dt>
    
            <dd>
                @Html.DisplayFor(Function(model) model.Value)
            </dd>
    
            <dt>
                @Html.DisplayNameFor(Function(model) model.IssuedBy)
            </dt>
    
            <dd>
                @Html.DisplayFor(Function(model) model.IssuedBy)
            </dd>
    
            <dt>
                @Html.DisplayNameFor(Function(model) model.Pf_Reference)
            </dt>
    
            <dd>
                @Html.DisplayFor(Function(model) model.Pf_Reference)
            </dd>
    
            <dt>
                @Html.DisplayNameFor(Function(model) model.M_Reference)
            </dt>
    
            <dd>
                @Html.DisplayFor(Function(model) model.M_Reference)
            </dd>
    
            <dt>
                @Html.DisplayNameFor(Function(model) model.Date)
            </dt>
    
            <dd>
                @Html.DisplayFor(Function(model) model.Date)
            </dd>
    
            <dt>
                @Html.DisplayNameFor(Function(model) model.Status)
            </dt>
    
            <dd>
                @Html.DisplayFor(Function(model) model.Status)
            </dd>
    
            <dt>
                @Html.DisplayNameFor(Function(model) model.UsedBy)
            </dt>
    
            <dd>
                @Html.DisplayFor(Function(model) model.UsedBy)
            </dd>
    
            <dt>
                @Html.DisplayNameFor(Function(model) model.UsedDate)
            </dt>
    
            <dd>
                @Html.DisplayFor(Function(model) model.UsedDate)
            </dd>
    
            <dt>
                @Html.DisplayNameFor(Function(model) model.UsedM_Reference)
            </dt>
    
            <dd>
                @Html.DisplayFor(Function(model) model.UsedM_Reference)
            </dd>
    
        </dl>
    </div>
    <p>
        @Html.ActionLink("Edit", "Edit", New With { .id = Model.VoucherID }) |
        @Html.ActionLink("Back to List", "Index")
    </p>
</body>
</html>
