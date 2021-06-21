using TMPro;

static class TMP_DropdownExtensions
{
    public static void SetAndRefresh(this TMP_Dropdown dd, int val)
    {
        dd.value = val;
        dd.RefreshShownValue();
    }
}