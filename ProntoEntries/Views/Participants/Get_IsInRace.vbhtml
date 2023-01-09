@If ViewBag.InRace = False Then

   @Html.ActionLink("Delete", "Delete", New With {.id = ViewBag.ParticipantID})
Else
   @<td>In Race</td> 
End If