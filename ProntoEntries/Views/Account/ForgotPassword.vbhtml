@ModelType ForgotPasswordViewModel
@Code
    ViewBag.Title = "Forgot your password?"
End Code


<div class="orgcontainer create">
    <div class="titlediv">
        <h2>@ViewBag.Title</h2>
        @Html.ActionLink("Return to Login", "Login")
    </div>

    <hr />

    @Using Html.BeginForm("ForgotPassword", "Account", FormMethod.Post, New With {.class = "form-horizontal", .role = "form"})
        @Html.AntiForgeryToken()
        @<text>
            @*<h4>Enter your email.</h4>
                <hr />*@
            @Html.ValidationSummary("", New With {.class = "text-danger"})
            <div class="form-group">
                @*@Html.LabelFor(Function(m) m.Email, New With {.class = "col-md-2 control-label"})*@
                <div class="col-md-10">
                    @Html.TextBoxFor(Function(m) m.Email, New With {.class = "form-control", .PlaceHolder = Html.DisplayNameFor(Function(model) model.Email)})
                </div>
            </div>

            <div class="button-group">
                <input type="submit" class="btn btn-default" value="Email Link" />
            </div>

            @*<div class="form-group">
                    <div class="col-md-offset-2 col-md-10">
                        <input type="submit" class="btn btn-default" value="Email Link" />
                    </div>
                </div>*@
        </text>
    End Using
</div>

    @section Scripts
        @Scripts.Render("~/bundles/jqueryval")
    End Section
