using System.Collections.Generic;

namespace Simplic.Package
{
    /// <summary>
    /// Interface to request a value from the user.
    /// <para>
    /// Target is that we will be able to request data from the user.
    /// </para>
    /// Problem with a simple Console.ReadLine are possible automated tests and a later implemented UI.
    /// <para>
    /// Also I would like to call the RequestValueService once for each object type and not for each object
    /// since it could get pretty anoying for the user when he has to fill 20 windows with 1 line instead of 1
    /// window with 20 lines.
    /// </para>
    /// </summary>
    public interface IRequestValueService
    {
        /// <summary>
        /// Requests the value for a list of installable objects.
        /// </summary>
        /// <param name="installableObjects">A list of installable objects of 1 type.</param>
        /// <returns>Wether the request was successfull.</returns>
        RequestValueResult RequestValue(IList<InstallableObject> installableObjects);
    }
}
