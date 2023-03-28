@ModelType ProntoEntries.Voucher

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
