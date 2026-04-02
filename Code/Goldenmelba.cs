using Sandbox;

public sealed class Goldenmelba : Component, Component.INetworkSpawn, Component.IPressable
{
	[Property]
	public SoundEvent WoofSound { get; set; }

	public Rigidbody Rb { get; private set; }

	protected override void OnAwake()
	{
		Rb = GetComponent<Rigidbody>();
	}

	protected override void OnStart()
	{
		SetRandomScale();
	}

	public bool Press( IPressable.Event e )
	{
		Woof();
		Jump();

		return true;
	}

	public void OnNetworkSpawn( Connection connection )
	{
		Network.AssignOwnership( Rpc.Caller );
	}

	private void Jump()
	{
		if ( !Rb.IsValid() )
			return;
		Rb.ApplyImpulse( GameObject.WorldRotation.Up * Game.Random.Float( 100000f, 500000f ) );
	}

	private void Woof()
	{
		if ( WoofSound.IsValid() )
			Sound.Play( WoofSound, WorldPosition );
	}

	private void SetRandomScale()
	{
		if ( IsProxy )
			return;
		WorldScale = Vector3.One * Game.Random.Float( 0.5f, 1f );
	}
}
