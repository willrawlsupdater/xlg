using System;
using System.Xml.Serialization;

namespace MetX.Standard.Techniques
{
    [Serializable]
    public class Technique : Particle
    {
        [XmlArray(ElementName = "Nodes")]
        [XmlArrayItem(typeof(NodeParticle), ElementName = "Node")]
        public ParticleList<NodeParticle> Nodes;
    }
}