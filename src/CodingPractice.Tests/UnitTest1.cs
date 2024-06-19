namespace CodingPractice.Tests
{
	public class UnitTest1
	{
		[Fact]
		public void Main_PrintsHelloWorld()
		{
			// Arrange
			var writer = new StringWriter();
			Console.SetOut(writer);

			// Act
			Program.Main(new string[0]);

			// Assert
			var output = writer.GetStringBuilder().ToString().Trim();
			Assert.Equal("Hello World!", output);
		}
	}
}
