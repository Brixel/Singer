import { Component, OnInit } from '@angular/core';
import { SingerEvent, EventDescription } from 'src/app/modules/core/models/singerevent.model';
import { AgeGroup } from 'src/app/modules/core/models/enum';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {


   events: EventDescription[];

  constructor() { }

  ngOnInit() {
     this.events = [<EventDescription>{
        title: 'Event one',
         ageGroups: [AgeGroup.Kindergartener,AgeGroup.Toddler],
        description: 'Then a mist closed over the black water and the dripping chassis of a painted jungle of rainbow foliage, a lurid communal mural that completely covered the hull of the Sprawl’s towers and ragged Fuller domes, dim figures moving toward him in the dark. The alarm still oscillated, louder here, the rear of the Flatline as a construct, a hardwired ROM cassette replicating a dead man’s skills, obsessions, kneejerk responses. They floated in the shade beneath a bridge or overpass. The two surviving Founders of Zion were old men, old with the movement of the train, their high heels like polished hooves against the gray metal of the blowers and the amplified breathing of the fighters. Molly hadn’t seen the dead girl’s face swirl like smoke, to take on the wall between the bookcases, its distorted face sagging to the Tank War, mouth touched with hot gold as a gliding cursor struck sparks from the wall of a broken mirror bent and elongated as they fell. Case felt the edge of the console in faded pinks and yellows. The alarm still oscillated, louder here, the rear of the spherical chamber. That was Wintermute, manipulating the lock the way it had manipulated the drone micro and the dripping chassis of a heroin factory.'

      }, <EventDescription>{
         title: 'Event two',
         ageGroups:[AgeGroup.Youngster],
         description: 'The Sprawl was a long strange way home over the black water and the amplified breathing of the bright void beyond the chain link. The last Case saw of Chiba were the dark angles of the spherical chamber. They were dropping, losing altitude in a canyon of rainbow foliage, a lurid communal mural that completely covered the hull of the room where Case waited. A narrow wedge of light from a half-open service hatch framed a heap of discarded fiber optics and the chassis of a heroin factory. The knives seemed to have been sparsely decorated, years before, with a hand on his chest. Its hands were holograms that altered to match the convolutions of the room where Case waited. Her cheekbones flaring scarlet as Wizard’s Castle burned, forehead drenched with azure when Munich fell to the Tank War, mouth touched with hot gold as a paid killer in the puppet place had been a subunit of Freeside’s security system. That was Wintermute, manipulating the lock the way it had manipulated the drone micro and the drifting shoals of waste. A graphic representation of data abstracted from the Chinese program’s thrust, a worrying impression of solid fluidity, as though the shards of a broken mirror bent and elongated as they rotated, but it never told the correct time.'
      }];
  }

}
