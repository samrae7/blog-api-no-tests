using Microsoft.EntityFrameworkCore;

namespace BlogApi.Models
{
  public class PostContext : DbContext
  {
    public PostContext(DbContextOptions<PostContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Post> Posts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Post>().HasData(
        new Post
          {
            Id = 1,
            Title = "William Shakespeare",
            Intro = "THis is an intro1",
            Body = "Sinister at creepy anxiety bite monster electrocution smashed in death. "
            + "Screams creaking tension kettle our exorcist. killer mental psychotic sliced. In the derange. "
            + "Halloween slice. Creep serial killer, bury a scourge menacing, pain bowels nightmare. "
            + "Occult at alley pushed. Shriek terror shadow, darkness in tense ac, Motionless drowning Full moon. "
            + "Haunt suicide silent, gory as demonic alarming, buried in fallen angel. "
            + "Sinister at creepy anxiety bite monster electrocution smashed in death. Needles at bowels alarming. "
            + "Falling a spooky screaming. Stalking wind, drenched chilling sick mental, with mutilatio. Zombies blood, "
            + " or shaking with willow trees shriek. Breathing heavily hell and rabid anthrax fanatic tearing at a squeal. "
            + "Evil Michael Myers decomposed corpse. Witch ashes eyeball undead, in bury burn hell flames. "
            + "Bloodcurdling decomposed zombie in virus scared cat Michael Myers worms. Drenched scream scared dark. "
            + "In horrifying, fear is gnarled murder, ominous eerie Serial killer sinister. "
            + "With sick chilling agony shaking heart pumping. Suicide Slash demon at convulsing darkness a evil pain burn. "
            + "Haunt tearing murder crying an mental corpse zombies evil, chainsaw motionless gory. "
            + "Eyeball cat silent, morbid in damp torture, 666 at brains. chainsaw dread full moon, "
            + "pushed at alley bruises, children is knife. Drenched scream scared dark. Psycho crazy mental hospital moon. "
            + "Bloodcurdlin. Decomposed zombie in virus scared cat Michael Myers worms. "
            + "Witch ashes eyeball undead, in bury burn hell flames. chainsaw dread full moon, pushed at alley bruises, children is knife. "
            + "In horrifying, fear is gnarled murder."
          },
        new Post
          {
            Id = 2,
            Title = "Jane Austen",
            Intro = "THis is an intro2",
            Body = "Proin lacus metus, egestas nec gravida a, feugiat ac elit. Sed dignissim neque lectus, eget elementum odio laoreet non. Curabitur viverra ornare nisi, commodo rutrum nunc dapibus eu. Aenean porta, velit in porta luctus, massa erat suscipit dui, vitae ultrices ante metus eget ligula. Donec tincidunt, mauris at iaculis iaculis, elit mauris aliquet turpis, in vulputate ante enim nec massa. Fusce vel consequat justo. Aliquam id rutrum risus. Aenean aliquet tincidunt lorem, ac rutrum urna posuere vel. Phasellus suscipit rutrum tortor, et aliquam arcu lacinia vitae. Etiam tristique vel tellus vel malesuada."
            + "Sed eu commodo augue, nec dignissim tortor. Duis vel nisl laoreet, venenatis diam non, laoreet mi. Nam vehicula ut metus bibendum eleifend. Ut mattis faucibus sollicitudin. Vivamus eget commodo neque. Fusce id mauris sit amet odio pretium accumsan non a risus. Quisque venenatis, est nec commodo sodales, dolor nisl tristique est, vel fringilla velit lacus vel magna. Vestibulum elementum vestibulum commodo. Nam semper mi ante, eget condimentum nisi elementum quis. Donec tempor feugiat diam eu imperdiet. Pellentesque at finibus metus, quis tempor nunc. Integer vehicula interdum commodo. Aenean nibh nibh, tempus non commodo a, lacinia quis est. Ut ultricies turpis vestibulum nulla congue venenatis."
            + "In hac habitasse platea dictumst. Donec et efficitur libero. Etiam sed massa non orci gravida elementum. In a semper metus. Donec finibus odio ac massa condimentum ultrices. Vivamus imperdiet eros quis lectus mattis consectetur ut non ligula. Proin varius aliquam turpis, sed ultricies velit. Morbi massa lacus, tincidunt et diam non, pulvinar cursus lorem."
          },
        new Post
          {
            Id = 3,
            Title = "Evelyn Waugh",
            Intro = "THis is an intro3",
            Body = "Lorem ipsum dolizzle sit boofron, crackalackin get down get down brizzle. Pizzle boom shackalack velizzle, aliquet volutpizzle, suscipit quis, funky fresh vizzle, daahng dawg. Pellentesque tellivizzle mofo. Sizzle eros. Bizzle things dolor dapibizzle turpizzle tempizzle fo. Hizzle pellentesque nibh et izzle. Dawg in yo mamma. Pellentesque izzle rhoncizzle shizzlin dizzle. In black habitasse yo mamma dictumst. Shizzle my nizzle crocodizzle own yo'. Curabitizzle tellus shizzle my nizzle crocodizzle, pretium eu, mattizzle shizzlin dizzle, eleifend vitae, nunc. Daahng dawg suscipizzle. Integer sempizzle yo "
            + "mamma sizzle dawg."
          },
        new Post
          {
            Id = 4,
            Title = "George Eliot",
            Intro = "THis is an intro4",
            Body = "Cake danish chocolate tootsie roll biscuit jelly dessert chocolate cake. Sweet jelly beans candy canes macaroon cake danish danish. Candy canes apple pie pastry."
            + "Donut topping jelly beans cupcake dragée cake brownie. Cheesecake donut fruitcake donut carrot cake dessert chupa chups. Carrot cake jujubes powder. Cookie oat cake macaroon tiramisu carrot cake chocolate bar."
            + "Soufflé tart muffin jelly-o wafer gingerbread sugar plum. Jujubes jelly icing fruitcake oat cake toffee chocolate chocolate bar. Sesame snaps wafer sweet."
          },
        new Post
          {
            Id = 5,
            Title = "Charles Dickens",
            Intro = "THis is an intro5",
            Body = "Praesent non mi non maurizzle fo shizzle bibendizzle. Aliquam lacinia viverra lectus. Cras id my shizz izzle leo sodalizzle dang. Aliquam pot, ghetto vitae dapibus sizzle"
            + ", nulla ligula bibendum yo, phat venenatis augue fo shizzle in arcu. Vivamizzle gravida lacizzle id mofo. Vivamizzle arcu magna, fermentizzle mammasay mammasa mamma oo sa amet, "
            + "sizzle izzle, mofo izzle, mauris. Sizzle vehicula laorizzle da bomb. Vestibulizzle nizzle dizzle, yo away, fo shizzle izzle, fo shizzle yo mamma, arcu. Morbi shiz placerizzle nulla. "
            + "For sure malesuada erizzle i'm in the shizzle erat. Yo metus sizzle, egestizzle gizzle, yippiyo quizzle, elementum crunk, neque. Doggy iaculizzle a orci fizzle mofo. Fusce sagittis, "
            + "nulla da bomb sollicitudin break it down, lacizzle fo shizzle mah nizzle fo rizzle, mah home g-dizzle luctus erat, vitae phat augue daahng dawg yippiyo yippiyo. Etizzle shiz lacizzle. "
            + "For sure shut the shizzle up mi. Duis gangsta turpizzle. Vestibulizzle the bizzle mah nizzle. Sizzle turpis erizzle, consectetizzle id, tempor izzle, sizzle pimpin', pede. Hizzle tellizzle. "
            + "Shizznit nisi erizzle, pot sit amizzle, ultricizzle i saw beyonces tizzles and my pizzle went crizzle, tincidunt non, black."
          }
      );
    }
  }
}