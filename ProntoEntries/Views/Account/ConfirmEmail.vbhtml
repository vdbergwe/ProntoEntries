@Code
    ViewBag.Title = "Confirm Email"
End Code

<div class="orgcontainer create">
    <div class="titlediv">
        <h2>@ViewBag.Title</h2>
    </div>
    <hr />

    <p style="text-align:center;">
        Thank you for confirming your email. Please @Html.ActionLink("Click here to Log in", "Login", "Account", routeValues:=Nothing, htmlAttributes:=New With {Key .id = "loginLink"})
  </p>

</div>

@*<h2>@ViewBag.Title.</h2>
<div>
    <p>
        Thank you for confirming your email. Please @Html.ActionLink("Click here to Log in", "Login", "Account", routeValues:=Nothing, htmlAttributes:=New With {Key .id = "loginLink"})
    </p>
</div>*@
