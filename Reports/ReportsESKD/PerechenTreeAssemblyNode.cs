internal class PerechenTreeAssemblyNode : Spisok
{
    private AssemblyComponentID subAssembly;

    public PerechenTreeAssemblyNode(AssemblyComponentID subAssembly) : base(name,)
    {
        this.subAssembly = subAssembly;
        if (this.subAssembly.ElementaryComponentID.Count == 0)
            return;
    }
}