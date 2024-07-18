using System.Diagnostics;
using Customer.Shared.Enums;

namespace Customer.Shared.Utils;

public class CustomerUtils
{
    public ProfileTypes GetProfileType(int profileType) => profileType switch
    {
        0 => ProfileTypes.ADMIN,
        1 => ProfileTypes.CLIENT,
        _ => throw new Exception("Invalid profile type")
    };
    
    public static bool ValidationProfileType(int profileType) => profileType == 0 || profileType == 1;
}