using Unity.VisualScripting;
using UnityEngine;


public class StatusEffectSlowness : MonoBehaviour, IEffectable
{
    [SerializeField] private FirstPersonController player;

    [SerializeField] private float newWalkSpeed;
    [SerializeField] private float recoverySpeed;

    private float normalWalkSpeed;

    public bool isActive {  get; private set; }

    public bool isRemedied { get; private set; }

    public string effectName { get; set; } = "Slowness";


    private void Start()
    {
        normalWalkSpeed = player.walkSpeed;
    }

    public void EndEffect()
    {
        isActive = false;
        player.walkSpeed = normalWalkSpeed;
    }

    private void Update()
    {
        if (isActive)
        {
            player.walkSpeed = Mathf.MoveTowards(player.walkSpeed, normalWalkSpeed, recoverySpeed * Time.deltaTime);
            Debug.Log(player.walkSpeed);

            if (player.walkSpeed == normalWalkSpeed)
            {
                isRemedied = true;
            }
        }
    }

    public void StartEffect()
    {
        isActive = true;

        player.walkSpeed = newWalkSpeed;
    }

}
