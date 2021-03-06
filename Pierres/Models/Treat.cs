using System.Collections.Generic;

namespace Pierres.Models
{
  public class Treat
  {
    public Treat()
    {
      this.JoinEntities = new HashSet<FlavorTreat>();
    }

    public int TreatId { get; set; }
    public string Name { get; set; }
    public int Rating { get; set; }
    public string About { get; set; }
    public virtual ApplicationUser User { get; set; }
    public bool Shared { get; set; }

    public virtual ICollection<FlavorTreat> JoinEntities { get; }
  }
}