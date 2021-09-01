/************************************************************************************

See SampleFramework license.txt for license terms.  Unless required by applicable law 
or agreed to in writing, the sample code is provided “AS IS” WITHOUT WARRANTIES OR 
CONDITIONS OF ANY KIND, either express or implied.  See the license for specific 
language governing permissions and limitations under the license.

************************************************************************************/

using UnityEngine;
using System.Collections;

/// <summary>
/// This transition will move the player with no other side effects.
/// </summary>
public class TeleportTransitionInstant : TeleportTransition
{
    /// <summary>
    /// When the teleport state is entered, simply move the player to the new location
    /// without any delay or other side effects.
    /// </summary>
    /// 

    //Masaki add Audio
    public AudioSource audioData;

    protected override void LocomotionTeleportOnEnterStateTeleporting()
	{
        //Masaki add Remove Audio
        audioData.Play();

        LocomotionTeleport.DoTeleport();
	}
}
