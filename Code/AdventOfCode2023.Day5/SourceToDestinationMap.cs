using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day5
{
  public class SourceToDestinationMap
  {
    public MapType DestinationType { get; set; }
    public MapType SourceType { get; set; }

    public long DestinationStart  { get; set; }
    public long SourceStart  { get; set; }
    public long Range { get; set; }
  }
}
