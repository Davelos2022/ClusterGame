using UnityEngine;

namespace MulticastProject.Models
{
    /// <summary>
    /// Represents a letter cluster data.
    /// </summary>
    public class ClusterData
    {
        public string Cluster { get; private set; }
        public Vector2 Position { get; set; }

        public ClusterData(string cluster)
        {
            Cluster = cluster;
            Position = Vector2.zero;
        }
    }
}
