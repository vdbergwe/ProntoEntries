@Code
    ViewBag.Title = "Reset password confirmation"
End Code


<div class="orgcontainer create">
    <div class="titlediv">
        <h2>@ViewBag.Title</h2>
    </div>
    <hr />


    <p style="text-align:center;">
        Your password has been reset. Please @Html.ActionLink("click here to log in", "Login", "Account", routeValues:=Nothing, htmlAttributes:=New With {Key .id = "loginLink"})
    </p>

</div>

    @*<hgroup class="title">
        <h1>@ViewBag.Title.</h1>
    </hgroup>
    <div>
        <p>
            Your password has been reset. Please @Html.ActionLink("click here to log in", "Login", "Account", routeValues:=Nothing, htmlAttributes:=New With {Key .id = "loginLink"})
        </p>
    </div>*@
