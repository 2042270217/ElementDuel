using System;

public readonly struct ElementReactionKey : IEquatable<ElementReactionKey>
{
	public readonly ElementType A;
	public readonly ElementType B;

	public ElementReactionKey(ElementType a, ElementType b)
	{
		// 无序组合：Fire + Water == Water + Fire
		if ((int)a <= (int)b)
		{
			A = a;
			B = b;
		}
		else
		{
			A = b;
			B = a;
		}
	}

	public bool Equals(ElementReactionKey other) => A == other.A && B == other.B;
	public override bool Equals(object obj) => obj is ElementReactionKey other && Equals(other);
	public override int GetHashCode() => HashCode.Combine(A, B);
}
