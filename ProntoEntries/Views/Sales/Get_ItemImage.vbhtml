@If (ViewBag.Image.Length > 0) Then
        @<img src="@Url.Content(ViewBag.Image)" class="eventimg" />
End If
