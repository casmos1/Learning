  //--------------------------------------------------------
  // Function to format SSN as the user types in the field.
  //--------------------------------------------------------
function jsFormatSSN(asSSNControl) {
  // Remove any characters that are not numbers
  var re = /\D/g; 
      
  var lvSSNControl = $get(asSSNControl.id);


var lvNumber = lvSSNControl.value.replace(re, "");
  var lvLength = lvNumber.length;
  // Fill the first dash for the user
  if(lvLength > 3 && lvLength < 6)
  {
    var lvSegmentA = lvNumber.slice(0,3);
    var lvSegmentB = lvNumber.slice(3,5);
    lvSSNControl.value = lvSegmentA + "-" + lvSegmentB;
  }
  else
  {
    // Fill the dashes for the user
    if(lvLength > 5)
    {
      var lvSegmentA = lvNumber.slice(0,3);
      var lvSegmentB = lvNumber.slice(3,5);
      var lvSegmentC = lvNumber.slice(5,9);
      lvSSNControl.value = lvSegmentA + "-" + lvSegmentB + "-" + lvSegmentC;
    }
    else
    {
      // If nothing is entered, leave the field blank
      if (lvLength < 1)
      {
        lvSSNControl.value = "";
      }
      lvSSNControl.value = lvNumber;
    }
  }
}