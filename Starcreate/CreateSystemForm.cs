using System;
using System.Drawing;
using System.Windows.Forms;

namespace Starcreate
{
  /// <summary>
  /// 
  /// </summary>
  public partial class CreateSystemForm : Form
  {
    private const double RADIANS_PER_ROTATION = 2.0 * Math.PI;
    private const double RAND_MAX = int.MaxValue;
    private const double ECCENTRICITY_COEFF = (.077); /* Dole's was 0.077 */
    private const double PROTOPLANET_MASS = (1.0E-15); /* Units of solar masses */
    private const double CHANGE_IN_EARTH_ANG_VEL = (-1.3E-15); /* Units of radians/sec/year */
    private const double SOLAR_MASS_IN_GRAMS = (1.989E33); /* Units of grams */
    private const double SOLAR_MASS_IN_KILOGRAMS = (1.989E30); /* Units of kg */
    private const double EARTH_MASS_IN_GRAMS = (5.977E27); /* Units of grams */
    private const double EARTH_RADIUS = (6.378E8); /* Units of cm */
    private const double EARTH_DENSITY = (5.52); /* Units of g/cc */
    private const double KM_EARTH_RADIUS = (6378.0); /* Units of km */
    private const double EARTH_ACCELERATION = (980.7); /* Units of cm/sec2 */
    private const double EARTH_AXIAL_TILT = (23.4); /* Units of degrees */
    private const double EARTH_EXOSPHERE_TEMP = (1273.0); /* Units of degrees Kelvin	*/
    private const double SUN_MASS_IN_EARTH_MASSES = (332775.64);
    private const double ASTEROID_MASS_LIMIT = (0.001); /* Units of Earth Masses */
    private const double EARTH_EFFECTIVE_TEMP = (250.0); /* Units of degrees Kelvin (was 255) */
    private const double CLOUD_COVERAGE_FACTOR = (1.839E-8); /* Km2/kg */
    private const double EARTH_WATER_MASS_PER_AREA = (3.83E15); /* grams per square km */
    private const double EARTH_SURF_PRES_IN_MILLIBARS = (1013.25);
    private const double EARTH_SURF_PRES_IN_MMHG = (760.0); /* Dole p. 15 */
    private const double EARTH_SURF_PRES_IN_PSI = (14.696); /* Pounds per square inch */
    private const double MMHG_TO_MILLIBARS = (EARTH_SURF_PRES_IN_MILLIBARS / EARTH_SURF_PRES_IN_MMHG);
    private const double PSI_TO_MILLIBARS = (EARTH_SURF_PRES_IN_MILLIBARS / EARTH_SURF_PRES_IN_PSI);
    private const double H20_ASSUMED_PRESSURE = (47.0 * MMHG_TO_MILLIBARS); /* Dole p. 15 */
    private const double MIN_O2_IPP = (72.0 * MMHG_TO_MILLIBARS); /* Dole, p. 15 */
    private const double MAX_O2_IPP = (400.0 * MMHG_TO_MILLIBARS); /* Dole, p. 15 */
    private const double MAX_HE_IPP = (61000.0 * MMHG_TO_MILLIBARS);/* Dole, p. 16 */
    private const double MAX_NE_IPP = (3900.0 * MMHG_TO_MILLIBARS); /* Dole, p. 16 */
    private const double MAX_N2_IPP = (2330.0 * MMHG_TO_MILLIBARS); /* Dole, p. 16 */
    private const double MAX_AR_IPP = (1220.0 * MMHG_TO_MILLIBARS); /* Dole, p. 16 */
    private const double MAX_KR_IPP = (350.0 * MMHG_TO_MILLIBARS); /* Dole, p. 16 */
    private const double MAX_XE_IPP = (160.0 * MMHG_TO_MILLIBARS); /* Dole, p. 16 */
    private const double MAX_CO2_IPP = (7.0 * MMHG_TO_MILLIBARS); /* Dole, p. 16 */
    private const double MAX_HABITABLE_PRESSURE = (118 * PSI_TO_MILLIBARS); /* Dole, p. 16 */
    // The next gases are listed as poisonous in parts per million by volume at 1 atm:

    private const double PPM_PRSSURE = (EARTH_SURF_PRES_IN_MILLIBARS / 1000000.0);
    private const double MAX_F_IPP = (0.1 * PPM_PRSSURE); /* Dole, p. 18 */
    private const double MAX_CL_IPP = (1.0 * PPM_PRSSURE); /* Dole, p. 18 */
    private const double MAX_NH3_IPP = (100.0 * PPM_PRSSURE); /* Dole, p. 18 */
    private const double MAX_O3_IPP = (0.1 * PPM_PRSSURE); /* Dole, p. 18 */
    private const double MAX_CH4_IPP = (50000.0 * PPM_PRSSURE); /* Dole, p. 18 */
    private const double EARTH_CONVECTION_FACTOR = (0.43); /* from Hart, eq.20 */
    private const double FREEZING_POINT_OF_WATER = (273.15); /* Units of degrees Kelvin */
    private const double EARTH_AVERAGE_CELSIUS = (14.0); /* Average Earth Temperature */
    private const double EARTH_AVERAGE_KELVIN = (EARTH_AVERAGE_CELSIUS + FREEZING_POINT_OF_WATER);
    private const double DAYS_IN_A_YEAR = (365.256); /* Earth days per Earth year */
    private const double GAS_RETENTION_THRESHOLD = (6.0); /* ratio of esc vel to RMS vel */
    private const double ICE_ALBEDO = (0.7);
    private const double CLOUD_ALBEDO = (0.52);
    private const double GAS_GIANT_ALBEDO = (0.5); /* albedo of a gas giant */
    private const double AIRLESS_ICE_ALBEDO = (0.5);
    private const double EARTH_ALBEDO = (0.3);/* was .33 for a while */
    private const double GREENHOUSE_TRIGGER_ALBEDO = (0.20);
    private const double ROCKY_ALBEDO = (0.15);
    private const double ROCKY_AIRLESS_ALBEDO = (0.07);
    private const double WATER_ALBEDO = (0.04);
    private const double SECONDS_PER_HOUR = (3600.0);
    private const double CM_PER_AU = (1.495978707E13); /* number of cm in an AU */
    private const double CM_PER_KM = (1.0E5); /* number of cm in a km */
    private const double KM_PER_AU = (CM_PER_AU / CM_PER_KM);
    private const double CM_PER_METER = (100.0);
    private const double MILLIBARS_PER_BAR = (1000.00);
    private const double GRAV_CONSTANT = (6.672E-8); /* units of dyne cm2/gram2 */
    private const double MOLAR_GAS_CONST = (8314.41); /* units: g*m2/(sec2*K*mol) */
    private const double K = (50.0); /* K = gas/dust ratio */
    private const double B = (1.2E-5); /* Used in Crit_mass calc */
    private const double DUST_DENSITY_COEFF = (2.0E-3); /* A in Dole's paper */
    private const double ALPHA = (5.0); /* Used in density calcs */
    private const double N = (3.0); /* Used in density calcs */
    private const double J = (1.46E-19); /* Used in day-length calcs (cm2/sec2 g) */
    private const double INCREDIBLY_LARGE_NUMBER = (9.9999E37);

    /* Now for a few molecular weights (used for RMS velocity calcs): */
    /* This table is from Dole's book "Habitable Planets for Man", p. 38 */
    private const double ATOMIC_HYDROGEN = (1.0); /* H */
    private const double MOL_HYDROGEN = (2.0); /* H2 */
    private const double HELIUM = (4.0); /* He */
    private const double ATOMIC_NITROGEN = (14.0); /* N */
    private const double ATOMIC_OXYGEN = (16.0); /* O */
    private const double METHANE = (16.0); /* CH4 */
    private const double AMMONIA = (17.0); /* NH3 */
    private const double WATER_VAPOR = (18.0); /* H2O */
    private const double NEON = (20.2); /* Ne */
    private const double MOL_NITROGEN = (28.0); /* N2 */
    private const double CARBON_MONOXIDE = (28.0); /* CO */
    private const double NITRIC_OXIDE = (30.0); /* NO */
    private const double MOL_OXYGEN = (32.0); /* O2 */
    private const double HYDROGEN_SULPHIDE = (34.1); /* H2S */
    private const double ARGON = (39.9); /* Ar */
    private const double CARBON_DIOXIDE = (44.0); /* CO2 */
    private const double NITROUS_OXIDE = (44.0); /* N2O */
    private const double NITROGEN_DIOXIDE = (46.0); /* NO2 */
    private const double OZONE = (48.0); /* O3 */
    private const double SULPH_DIOXIDE = (64.1); /* SO2 */
    private const double SULPH_TRIOXIDE = (80.1); /* SO3 */
    private const double KRYPTON = (83.8); /* Kr */
    private const double XENON = (131.3); /* Xe */
    // And atomic numbers, for use in ChemTable indexes

    private const int AN_H = 1;
    private const int AN_HE = 2;
    private const int AN_N = 7;
    private const int AN_O = 8;
    private const int AN_F = 9;
    private const int AN_NE = 10;
    private const int AN_P = 15;
    private const int AN_CL = 17;
    private const int AN_AR = 18;
    private const int AN_BR = 35;
    private const int AN_KR = 36;
    private const int AN_I = 53;
    private const int AN_XE = 54;
    private const int AN_HG = 80;
    private const int AN_AT = 85;
    private const int AN_RN = 86;
    private const int AN_FR = 87;
    private const int AN_NH3 = 900;
    private const int AN_H2O = 901;
    private const int AN_CO2 = 902;
    private const int AN_O3 = 903;
    private const int AN_CH4 = 904;
    private const int AN_CH3CH2OH = 905;

    /* The following defines are used in the kothari_radius function in */
    /* file enviro.c. */
    private const double A1_20 = (6.485E12); /* All units are in cgs system. */
    private const double A2_20 = (4.0032E-8); /* ie: cm, g, dynes, etc. */
    private const double BETA_20 = (5.71E12);
    private const double JIMS_FUDGE = (1.004);

    /* The following defines are used in determining the fraction of a planet */
    /* covered with clouds in function cloud_fraction in file enviro.c. */
    private const double Q1_36 = (1.258E19); /* grams */
    private const double Q2_36 = (0.0698); /* 1/Kelvin */
    private const int NONE = 0;
    private const int BREATHABLE = 1;
    private const int UNBREATHABLE = 2;
    private const int POISONOUS = 3;
    private const int fUseSolarsystem = 0x0001;
    private const int fReuseSolarsystem = 0x0002;
    private const int fUseKnownPlanets = 0x0004;
    private const int fNoGenerate = 0x0008;
    private const int fDoGases = 0x0010;
    private const int fDoMoons = 0x0020;
    private const int fOnlyHabitable = 0x0100;
    private const int fOnlyMultiHabitable = 0x0200;
    private const int fOnlyJovianHabitabl = 0x0400;
    private const int fOnlyEarthlike = 0x0800;

    // Values of out_format
    private const string ffHTML = "HTML";
    private const string ffTEXT = "TEXT";
    private const string ffCELESTIA = ".SSC";
    private const string ffCSV = ".CSV";
    private const string ffCSVdl = "+CSV";
    private const string ffSVG = ".SVG";

    // Values of graphic_format
    private const string gfGIF = ".GIF";
    private const string gfSVG = ".SVG";
    private readonly int max_gas = 14;
    private bool dust_left;
    private double r_inner;
    private double r_outer;
    private double reduced_mass;
    private readonly double dust_density;
    private double cloud_eccentricity;
    private dust_record dust_head;
    private planet_record planet_head;
    private gen hist_head;

    /// <summary>
    /// 
    /// </summary>
    public enum planet_type
    {
      /// <summary>
      /// 
      /// </summary>
      tUnknown,
      /// <summary>
      /// 
      /// </summary>
      tRock,
      /// <summary>
      /// 
      /// </summary>
      tVenusian,
      /// <summary>
      /// 
      /// </summary>
      tTerrestrial,
      /// <summary>
      /// 
      /// </summary>
      tGasGiant,
      /// <summary>
      /// 
      /// </summary>
      tMartian,
      /// <summary>
      /// 
      /// </summary>
      tWater,
      /// <summary>
      /// 
      /// </summary>
      tIce,
      /// <summary>
      /// 
      /// </summary>
      tSubGasGiant,
      /// <summary>
      /// 
      /// </summary>
      tSubSubGasGiant,
      /// <summary>
      /// 
      /// </summary>
      tAsteroids,
      /// <summary>
      /// 
      /// </summary>
      t1Face
    };

    /// <summary>
    /// 
    /// </summary>
    public enum actions
    { // Callable StarGen can:
      /// <summary>
      /// 
      /// </summary>
      aGenerate, //	- Generate randon system(s)
      /// <summary>
      /// 
      /// </summary>
      aListGases, //	- List the gas table
      /// <summary>
      /// 
      /// </summary>
      aListCatalog, //	- List the stars in a catalog
      /// <summary>
      /// 
      /// </summary>
      aListCatalogAsHTML, //  - For creating a <FORM>
      /// <summary>
      /// 
      /// </summary>
      aSizeCheck, //  - List sizes of various types
      /// <summary>
      /// 
      /// </summary>
      aListVerbosity //  - List values of the -v option
    }

    /// <summary>
    /// 
    /// </summary>
    public struct gas
    {
      /// <summary>
      /// 
      /// </summary>
      public int num;
      /// <summary>
      /// 
      /// </summary>
      public double surf_pressure; /* units of millibars (mb) */
    }

    /// <summary>
    /// 
    /// </summary>
    public struct sun
    {
      public double luminosity;
      public double mass;
      public double life;
      public double age;
      public double r_ecosphere;
      public string name;
    }

    /// <summary>
    /// 
    /// </summary>
    public struct planet_record
    {
      public int planet_no;
      public double a;          /* semi-major axis of solar orbit (in AU)*/
      public double e;          /* eccentricity of solar orbit		 */
      public double axial_tilt;     /* units of degrees					 */
      public double mass;       /* mass (in solar masses)			 */
      public bool gas_giant;      /* TRUE if the planet is a gas giant */
      public double dust_mass;      /* mass, ignoring gas				 */
      public double gas_mass;     /* mass, ignoring dust				 */
      /*   ZEROES start here               */
      public double moon_a;       /* semi-major axis of lunar orbit (in AU)*/
      public double moon_e;       /* eccentricity of lunar orbit		 */
      public double core_radius;    /* radius of the rocky core (in km)	 */
      public double radius;       /* equatorial radius (in km)		 */
      public int orbit_zone;     /* the 'zone' of the planet			 */
      public double density;      /* density (in g/cc)				 */
      public double orb_period;     /* length of the local year (days)	 */
      public double day;        /* length of the local day (hours)	 */
      public bool resonant_period;  /* TRUE if in resonant rotation		 */
      public double esc_velocity;   /* units of cm/sec					 */
      public double surf_accel;     /* units of cm/sec2					 */
      public double surf_grav;      /* units of Earth gravities			 */
      public double rms_velocity;   /* units of cm/sec					 */
      public double molec_weight;   /* smallest molecular weight retained*/
      public double volatile_gas_inventory;
      public double surf_pressure;    /* units of millibars (mb)			 */
      public bool greenhouse_effect;  /* runaway greenhouse effect?		 */
      public double boil_point;     /* the boiling point of water (Kelvin)*/
      public double albedo;       /* albedo of the planet				 */
      public double exospheric_temp;  /* units of degrees Kelvin			 */
      public double estimated_temp;     /* quick non-iterative estimate (K)  */
      public double estimated_terr_temp;/* for terrestrial moons and the like*/
      public double surf_temp;      /* surface temperature in Kelvin	 */
      public double greenhs_rise;   /* Temperature rise due to greenhouse */
      public double high_temp;      /* Day-time temperature              */
      public double low_temp;     /* Night-time temperature			 */
      public double max_temp;     /* Summer/Day						 */
      public double min_temp;     /* Winter/Night						 */
      public double hydrosphere;    /* fraction of surface covered		 */
      public double cloud_cover;    /* fraction of surface covered		 */
      public double ice_cover;      /* fraction of surface covered		 */
      public sun sun;
      public int gases;        /* Count of gases in the atmosphere: */
      public gas atmosphere;
      public planet_type type;       /* Type code						 */
      public int minor_moons;
      //planet_pointer first_moon;
      /*   ZEROES end here               */
      //planet_record next_planet;
    }

    /// <summary>
    /// 
    /// </summary>
    public struct dust_record
    {
      public double inner_edge;
      public double outer_edge;
      public bool dust_present;
      public bool gas_present;
      //dust_record next_band;
    }

    /// <summary>
    /// 
    /// </summary>
    public struct star
    {
      public double luminosity;
      public double mass;
      public double m2;
      public double e;
      public double a;
      public planet_record[] known_planets;
      public string desig;
      public int in_celestia;
      public string name;
    }

    /// <summary>
    /// 
    /// </summary>
    public struct catalog
    {
      public int count;
      public string arg;
      public star[] stars;
    }

    /// <summary>
    /// 
    /// </summary>
    public struct gen
    {
      public dust_record dusts;
      public planet_record planets;
      //gen_record next;
    }

    /// <summary>
    /// 
    /// </summary>
    public struct ChemTable
    {
      public int num;
      public string symbol;
      public string html_symbol;
      public string name;
      public double weight;
      public double melt;
      public double boil;
      public double density;
      public double abunde;
      public double abunds;
      public double reactivity;
      public double max_ipp; // Max inspired partial pressure im millibars
    }

    /// <summary>
    /// 
    /// </summary>
    public string[] breathability_phrase =
    {
      "none",
      "breathable",
      "unbreathable",
      "poisonous"
    };

    /// <summary>
    /// 
    /// </summary>
    private readonly ChemTable[] gases = new ChemTable[14];

    /// <summary>
    /// This is the constructor of the class 'CreateSystemForm'.
    /// </summary>
    public CreateSystemForm()
    {
      InitializeComponent();
      gases[0].num = AN_H;
      gases[0].symbol = "H";
      gases[0].html_symbol = "H<SUB><SMALL>2</SMALL></SUB>";
      gases[0].name = "Hydrogen";
      gases[0].weight = 1.0079;
      gases[0].melt = 14.06;
      gases[0].boil = 20.40;
      gases[0].density = 8.99e-05;
      gases[0].abunde = 0.00125893;
      gases[0].abunds = 27925.4;
      gases[0].reactivity = 1;
      gases[0].max_ipp = 0.0;
      gases[1].num = AN_HE; gases[1].symbol = "He"; gases[1].html_symbol = "He"; gases[1].name = "Helium"; gases[1].weight = 4.0026; gases[1].melt = 3.46; gases[1].boil = 4.20; gases[1].density = 0.0001787; gases[1].abunde = 7.94328e-09; gases[1].abunds = 2722.7; gases[1].reactivity = 0; gases[1].max_ipp = MAX_HE_IPP;
      gases[2].num = AN_N; gases[2].symbol = "N"; gases[2].html_symbol = "N<SUB><SMALL>2</SMALL></SUB>"; gases[2].name = "Nitrogen"; gases[2].weight = 14.0067; gases[2].melt = 63.34; gases[2].boil = 77.40; gases[2].density = 0.0012506; gases[2].abunde = 1.99526e-05; gases[2].abunds = 3.13329; gases[2].reactivity = 0; gases[2].max_ipp = MAX_N2_IPP;
      gases[3].num = AN_O; gases[3].symbol = "O"; gases[3].html_symbol = "O<SUB><SMALL>2</SMALL></SUB>"; gases[3].name = "Oxygen"; gases[3].weight = 15.9994; gases[3].melt = 54.80; gases[3].boil = 90.20; gases[3].density = 0.001429; gases[3].abunde = 0.501187; gases[3].abunds = 23.8232; gases[3].reactivity = 10; gases[3].max_ipp = MAX_O2_IPP;
      gases[4].num = AN_NE; gases[4].symbol = "Ne"; gases[4].html_symbol = "Ne"; gases[4].name = "Neon"; gases[4].weight = 20.1700; gases[4].melt = 24.53; gases[4].boil = 27.10; gases[4].density = 0.0009; gases[4].abunde = 5.01187e-09; gases[4].abunds = 3.4435e-5; gases[4].reactivity = 0; gases[4].max_ipp = MAX_NE_IPP;
      gases[5].num = AN_AR; gases[5].symbol = "Ar"; gases[5].html_symbol = "Ar"; gases[5].name = "Argon"; gases[5].weight = 39.9480; gases[5].melt = 84.00; gases[5].boil = 87.30; gases[5].density = 0.0017824; gases[5].abunde = 3.16228e-06; gases[5].abunds = 0.100925; gases[5].reactivity = 0; gases[5].max_ipp = MAX_AR_IPP;
      gases[6].num = AN_KR; gases[6].symbol = "Kr"; gases[6].html_symbol = "Kr"; gases[6].name = "Krypton"; gases[6].weight = 83.8000; gases[6].melt = 116.60; gases[6].boil = 119.70; gases[6].density = 0.003708; gases[6].abunde = 1e-10; gases[6].abunds = 4.4978e-05; gases[6].reactivity = 0; gases[6].max_ipp = MAX_KR_IPP;
      gases[7].num = AN_XE; gases[7].symbol = "Xe"; gases[7].html_symbol = "Xe"; gases[7].name = "Xenon"; gases[7].weight = 131.3000; gases[7].melt = 161.30; gases[7].boil = 165.00; gases[7].density = 0.00588; gases[7].abunde = 3.16228e-11; gases[7].abunds = 4.69894e-06; gases[7].reactivity = 0; gases[7].max_ipp = MAX_XE_IPP;
      gases[8].num = AN_NH3; gases[8].symbol = "NH3"; gases[8].html_symbol = "NH<SUB><SMALL>3</SMALL></SUB>"; gases[8].name = "Ammonia"; gases[8].weight = 17.0000; gases[8].melt = 195.46; gases[8].boil = 239.66; gases[8].density = 0.001; gases[8].abunde = 0.002; gases[8].abunds = 0.0001; gases[8].reactivity = 1; gases[8].max_ipp = MAX_NH3_IPP;
      gases[9].num = AN_H2O; gases[9].symbol = "H2O"; gases[9].html_symbol = "H<SUB><SMALL>2</SMALL></SUB>O"; gases[9].name = "Water"; gases[9].weight = 18.0000; gases[9].melt = 273.16; gases[9].boil = 373.16; gases[9].density = 1.000; gases[9].abunde = 0.03; gases[9].abunds = 0.001; gases[9].reactivity = 0; gases[9].max_ipp = 0.0;
      gases[10].num = AN_CO2; gases[10].symbol = "CO2"; gases[10].html_symbol = "CO<SUB><SMALL>2</SMALL></SUB>"; gases[10].name = "CarbonDioxide"; gases[10].weight = 44.0000; gases[10].melt = 194.66; gases[10].boil = 194.66; gases[10].density = 0.001; gases[10].abunde = 0.01; gases[10].abunds = 0.0005; gases[10].reactivity = 0; gases[10].max_ipp = MAX_CO2_IPP;
      gases[11].num = AN_O3; gases[11].symbol = "O3"; gases[11].html_symbol = "O<SUB><SMALL>3</SMALL></SUB>"; gases[11].name = "Ozone"; gases[11].weight = 48.0000; gases[11].melt = 80.16; gases[11].boil = 161.16; gases[11].density = 0.001; gases[11].abunde = 0.001; gases[11].abunds = 0.000001; gases[11].reactivity = 2; gases[11].max_ipp = MAX_O3_IPP;
      gases[12].num = AN_CH4; gases[12].symbol = "CH4"; gases[12].html_symbol = "CH<SUB><SMALL>4</SMALL></SUB>"; gases[12].name = "Methane"; gases[12].weight = 16.0000; gases[12].melt = 90.16; gases[12].boil = 109.16; gases[12].density = 0.010; gases[12].abunde = 0.005; gases[12].abunds = 0.0001; gases[12].reactivity = 1; gases[12].max_ipp = MAX_CH4_IPP;
      gases[13].num = 0; gases[12].symbol = ""; gases[13].html_symbol = ""; gases[13].name = ""; gases[13].weight = 0; gases[13].melt = 0; gases[13].boil = 0; gases[13].density = 0; gases[13].abunde = 0; gases[13].abunds = 0; gases[13].reactivity = 0; gases[13].max_ipp = 0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    private double Pow2(double a)
    {
      return Math.Pow(a, 2);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    private double Pow3(double a)
    {
      return Math.Pow(a, 3);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    private double Pow4(double a)
    {
      return Math.Pow(a, 4);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    private double Pow1_4(double a)
    {
      return Math.Pow(a, 0.15);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    private double Pow1_3(double a)
    {
      return Math.Pow(a, (1.0 / 3.0));
    }

    /// <summary>
    /// This function returns a random real number between the specified inner and outer bounds.
    /// </summary>
    /// <returns></returns>
    private double randomNumber(double inner, double outer)
    {
      double range;
      range = outer - inner;
      Random _r = new Random();
      double result = (_r.Next() / RAND_MAX) * range + inner;
      return result;
    }

    /// <summary>
    /// This function returns a value within a certain variation of the exact value given it in 'value'.
    /// </summary>
    /// <returns></returns>
    private double about(double value, double variation)
    {
      double result = value + (value * randomNumber(-variation, variation));
      return result;
    }

    /// <summary>
    /// blabla
    /// </summary>
    /// <param name="value"></param>
    /// <param name="variation"></param>
    /// <returns></returns>
    private double randomEccentricity(double value, double variation)
    {
      //double result = 1.0 - Math.Pow(randomNumber(0.0, 1.0), ECCENTRICITY_COEFF);
      double result = 1.0 - Math.Pow(randomNumber(0.0, 1.0), (double)numericUpDownEccentricityCoeff.Value);
      if (result > 0.99)
      {
        result = 0.99;
      }

      return result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mass_ratio"></param>
    /// <returns></returns>
    private double luminosity(double mass_ratio)
    {
      double n;
      if (mass_ratio < 1.0)
      {
        n = 1.75 * (mass_ratio - 0.1) + 3.325;
      }
      else
      {
        n = 0.5 * (2.0 - mass_ratio) + 4.4;
      }

      return Math.Pow(mass_ratio, n);
    }

    /// <summary>
    /// This function, given the orbital radius of a planet in AU, returns the orbital 'zone' of the particle.
    /// </summary>
    /// <param name="luminosity"></param>
    /// <param name="orb_radius"></param>
    /// <returns></returns>
    private int orb_zone(double luminosity, double orb_radius)
    {
      if (orb_radius < (4.0 * Math.Sqrt(luminosity)))
      {
        return (1);
      }
      else if (orb_radius < (15.0 * Math.Sqrt(luminosity)))
      {
        return (2);
      }
      else
      {
        return (3);
      }
    }

    /// <summary>
    /// The mass is in units of solar masses, and the density is in units of grams/cc.  The radius returned is in units of km.
    /// </summary>
    /// <param name="mass"></param>
    /// <param name="density"></param>
    /// <returns></returns>
    private double volume_radius(double mass, double density)
    {
      double volume;
      mass = mass * SOLAR_MASS_IN_GRAMS;
      volume = mass / density;
      return Math.Pow((3.0 * volume) / (4.0 * Math.PI), (1.0 / 3.0)) / CM_PER_KM;
    }

    /// <summary>
    /// Returns the radius of the planet in kilometers.
    /// The mass passed in is in units of solar masses.
    /// This formula is listed as eq.9 in Fogg's article, although some typos	crop up in that eq. See "The Internal Constitution of Planets", by Dr. D. S. Kothari, Mon. Not. of the Royal Astronomical Society, vol 96 pp.833-843, 1936 for the derivation. Specifically, this is Kothari's eq.23, which appears on page 840.
    /// </summary>
    /// <param name="mass"></param>
    /// <param name="giant"></param>
    /// <param name="zone"></param>
    /// <returns></returns>
    private double kothari_radius(double mass, int giant, int zone)
    {
      /*volatile*/
      double temp1;
      double temp, temp2, atomic_weight, atomic_num;
      if (zone == 1)
      {
        if (giant == 1)
        {
          atomic_weight = 9.5;
          atomic_num = 4.5;
        }
        else
        {
          atomic_weight = 15.0;
          atomic_num = 8.0;
        }
      }
      else if (zone == 2)
      {
        if (giant == 1)
        {
          atomic_weight = 2.47;
          atomic_num = 2.0;
        }
        else
        {
          atomic_weight = 10.0;
          atomic_num = 5.0;
        }
      }
      else
      {
        if (giant == 1)
        {
          atomic_weight = 7.0;
          atomic_num = 4.0;
        }
        else
        {
          atomic_weight = 10.0;
          atomic_num = 5.0;
        }
      }
      temp1 = atomic_weight * atomic_num;
      temp = (2.0 * BETA_20 * Math.Pow(SOLAR_MASS_IN_GRAMS, (1.0 / 3.0))) / (A1_20 * Math.Pow(temp1, (1.0 / 3.0)));
      temp2 = A2_20 * Math.Pow(atomic_weight, (4.0 / 3.0)) * Math.Pow(SOLAR_MASS_IN_GRAMS, (2.0 / 3.0));
      temp2 = temp2 * Math.Pow(mass, (2.0 / 3.0));
      temp2 = temp2 / (A1_20 * Pow2(atomic_num));
      temp2 = 1.0 + temp2;
      temp = temp / temp2;
      temp = (temp * Math.Pow(mass, (1.0 / 3.0))) / CM_PER_KM;
      temp /= JIMS_FUDGE; /* Make Earth = actual earth */
      return temp;
    }

    /// <summary>
    /// The mass passed in is in units of solar masses, and the orbital radius is in units of AU.	The density is returned in units of grams/cc.
    /// </summary>
    /// <param name="mass"></param>
    /// <param name="orb_radius"></param>
    /// <param name="r_ecosphere"></param>
    /// <param name="gas_giant"></param>
    /// <returns></returns>
    private double empirical_density(double mass, double orb_radius, double r_ecosphere, int gas_giant)
    {
      double temp;
      temp = Math.Pow(mass * SUN_MASS_IN_EARTH_MASSES, (1.0 / 8.0));
      temp = temp * Pow1_4(r_ecosphere / orb_radius);
      if (gas_giant == 1)
      {
        return (temp * 1.2);
      }
      else
      {
        return (temp * 5.5);
      }
    }

    /// <summary>
    /// The mass passed in is in units of solar masses, and the equatorial radius is in km.  The density is returned in units of grams/cc.
    /// </summary>
    /// <param name="mass"></param>
    /// <param name="equat_radius"></param>
    /// <returns></returns>
    private double volume_density(double mass, double equat_radius)
    {
      double volume;
      mass = mass * SOLAR_MASS_IN_GRAMS;
      equat_radius = equat_radius * CM_PER_KM;
      volume = (4.0 * Math.PI * Pow3(equat_radius)) / 3.0;
      return (mass / volume);
    }

    /// <summary>
    /// The separation is in units of AU, and both masses are in units of solar masses.	The period returned is in terms of Earth days.
    /// </summary>
    /// <param name="separation"></param>
    /// <param name="small_mass"></param>
    /// <param name="large_mass"></param>
    /// <returns></returns>
    private double period(double separation, double small_mass, double large_mass)
    {
      double period_in_years;
      period_in_years = Math.Sqrt(Pow3(separation) / (small_mass + large_mass));
      return (period_in_years * DAYS_IN_A_YEAR);
    }

    /// <summary>
    /// Fogg's information for this routine came from Dole "Habitable Planets for Man", Blaisdell Publishing Company, NY, 1964. From this, he came up with his eq.12, which is the equation for the 'base_angular_velocity'. He then used an equation for the change in angular velocity pertime (dw/dt) from P. Goldreich and S. Soter's paper "Q in the Solar System" in Icarus, vol 5, pp.375-389 (1966). Using as a comparison the change in angular velocity for the Earth, Fogg has come up with an approximation for our new planet (his eq. 13) and take that into account. This is used to find 'change_in_angular_velocity' below.
    /// Input parameters are mass (in solar masses), radius (in Km), orbital period (in days), orbital radius (in AU), density (in g/cc), eccentricity, and whether it is a gas giant or not.
    /// The length of the day is returned in units of hours.
    /// </summary>
    /// <param name="planet"></param>
    /// <returns></returns>
    private double day_length(planet_record planet)
    {
      double planetary_mass_in_grams = planet.mass * SOLAR_MASS_IN_GRAMS;
      double equatorial_radius_in_cm = planet.radius * CM_PER_KM;
      double year_in_hours = planet.orb_period * 24.0;
      bool giant = (planet.type == planet_type.tGasGiant || planet.type == planet_type.tSubGasGiant || planet.type == planet_type.tSubSubGasGiant);
      double k2;
      double base_angular_velocity;
      double change_in_angular_velocity;
      double ang_velocity;
      double spin_resonance_factor;
      double day_in_hours;
      bool stopped = false;
      planet.resonant_period = false;  /* Warning: Modify the planet */
      if (giant)
      {
        k2 = 0.24;
      }
      else
      {
        k2 = 0.33;
      }

      base_angular_velocity = Math.Sqrt(2.0 * J * (planetary_mass_in_grams) / (k2 * Pow2(equatorial_radius_in_cm)));
      /* This next calculation determines how much the planet's rotation is lowed by the presence of the star. */
      change_in_angular_velocity = CHANGE_IN_EARTH_ANG_VEL * (planet.density / EARTH_DENSITY) * (equatorial_radius_in_cm / EARTH_RADIUS) * (EARTH_MASS_IN_GRAMS / planetary_mass_in_grams) * Math.Pow(planet.sun.mass, 2.0) * (1.0 / Math.Pow(planet.a, 6.0));
      ang_velocity = base_angular_velocity + (change_in_angular_velocity * planet.sun.age);
      /* Now we change from rad/sec to hours/rotation. */
      if (ang_velocity <= 0.0)
      {
        stopped = true;
        day_in_hours = INCREDIBLY_LARGE_NUMBER;
      }
      else
      {
        day_in_hours = RADIANS_PER_ROTATION / (SECONDS_PER_HOUR * ang_velocity);
      }

      if ((day_in_hours >= year_in_hours) || stopped)
      {
        if (planet.e > 0.1)
        {
          spin_resonance_factor = (1.0 - planet.e) / (1.0 + planet.e);
          planet.resonant_period = true;
          return (spin_resonance_factor * year_in_hours);
        }
        else
        {
          return year_in_hours;
        }
      }
      return day_in_hours;
    }

    /// <summary>
    /// The orbital radius is expected in units of Astronomical Units (AU).
    /// Inclination is returned in units of degrees.
    /// </summary>
    /// <param name="orb_radius"></param>
    /// <returns></returns>
    private int inclination(double orb_radius)
    {
      int temp;
      temp = (int)(Math.Pow(orb_radius, 0.2) * about(EARTH_AXIAL_TILT, 0.4));
      return temp % 360;
    }

    /// <summary>
    /// This function implements the escape velocity calculation. Note that it appears that Fogg's eq.15 is incorrect.
    /// The mass is in units of solar mass, the radius in kilometers, and the velocity returned is in cm/sec.
    /// </summary>
    /// <param name="mass"></param>
    /// <param name="radius"></param>
    /// <returns></returns>
    private double escape_vel(double mass, double radius)
    {
      double mass_in_grams, radius_in_cm;
      mass_in_grams = mass * SOLAR_MASS_IN_GRAMS;
      radius_in_cm = radius * CM_PER_KM;
      return Math.Sqrt(2.0 * GRAV_CONSTANT * mass_in_grams / radius_in_cm);
    }

    /// <summary>
    /// This is Fogg's eq.16. The molecular weight (usually assumed to be N2) is used as the basis of the Root Mean Square (RMS) velocity of the molecule or atom. The velocity returned is in cm/sec.
    /// Orbital radius is in A.U.(ie: in units of the earth's orbital radius).
    /// </summary>
    /// <param name="molecular_weight"></param>
    /// <param name="exospheric_temp"></param>
    /// <returns></returns>
    private double rms_vel(double molecular_weight, double exospheric_temp)
    {
      return Math.Sqrt((3.0 * MOLAR_GAS_CONST * exospheric_temp) / molecular_weight) * CM_PER_METER;
    }

    /// <summary>
    /// This function returns the smallest molecular weight retained by the body, which is useful for determining the atmosphere composition.
    /// Mass is in units of solar masses, and equatorial radius is in units of kilometers.
    /// </summary>
    /// <param name="mass"></param>
    /// <param name="equat_radius"></param>
    /// <param name="exospheric_temp"></param>
    /// <returns></returns>
    private double molecule_limit(double mass, double equat_radius, double exospheric_temp)
    {
      double esc_velocity = escape_vel(mass, equat_radius);
      return (3.0 * MOLAR_GAS_CONST * exospheric_temp) / (Pow2((esc_velocity / GAS_RETENTION_THRESHOLD) / CM_PER_METER));
    }

    /// <summary>
    /// This function calculates the surface acceleration of a planet.
    /// The mass is in units of solar masses, the radius in terms of km, and the acceleration is returned in units of cm/sec2.
    /// </summary>
    /// <param name="mass"></param>
    /// <param name="radius"></param>
    /// <returns></returns>
    private double acceleration(double mass, double radius)
    {
      return GRAV_CONSTANT * (mass * SOLAR_MASS_IN_GRAMS) / Pow2(radius * CM_PER_KM);
    }

    /// <summary>
    /// This function calculates the surface gravity of a planet.
    /// The acceleration is in units of cm/sec2, and the gravity is returned in acceleration is in units of cm/sec2, and the gravity is returned in units of Earth gravities.	
    /// </summary>
    /// <param name="acceleration"></param>
    /// <returns></returns>
    private double gravity(double acceleration)
    {
      return acceleration / EARTH_ACCELERATION;
    }

    /// <summary>
    /// This implements Fogg's eq.17.  The 'inventory' returned is unitless.
    /// </summary>
    /// <param name="mass"></param>
    /// <param name="escape_vel"></param>
    /// <param name="rms_vel"></param>
    /// <param name="stellar_mass"></param>
    /// <param name="zone"></param>
    /// <param name="greenhouse_effect"></param>
    /// <param name="accreted_gas"></param>
    /// <returns></returns>
    private double vol_inventory(double mass, double escape_vel, double rms_vel, double stellar_mass, int zone, bool greenhouse_effect, bool accreted_gas)
    {
      double velocity_ratio, proportion_const, temp1, temp2, earth_units;
      velocity_ratio = escape_vel / rms_vel;
      if (velocity_ratio >= GAS_RETENTION_THRESHOLD)
      {
        switch (zone)
        {
          case 1:
            proportion_const = 140000.0;  /* 100 -> 140 JLB */
            break;
          case 2:
            proportion_const = 75000.0;
            break;
          case 3:
            proportion_const = 250.0;
            break;
          default:
            proportion_const = 0.0;
            MessageBox.Show("Error: orbital zone not initialized correctly!");
            break;
        }
        earth_units = mass * SUN_MASS_IN_EARTH_MASSES;
        temp1 = (proportion_const * earth_units) / stellar_mass;
        temp2 = about(temp1, 0.2);
        temp2 = temp1;
        if (greenhouse_effect || accreted_gas)
        {
          return temp2;
        }
        else
        {
          return temp2 / 140.0; /* 100 -> 140 JLB */
        }
      }
      else
      {
        return 0.0;
      }
    }

    /// <summary>
    /// This implements Fogg's eq.18. The pressure returned is in units of millibars (mb). The gravity is in units of Earth gravities, the radius in units of kilometers.
    /// JLB: Aparently this assumed that earth pressure = 1000mb. I've added a fudge factor (EARTH_SURF_PRES_IN_MILLIBARS / 1000.) to correct for that.
    /// </summary>
    /// <param name="volatile_gas_inventory"></param>
    /// <param name="equat_radius"></param>
    /// <param name="gravity"></param>
    /// <returns></returns>
    private double pressure(double volatile_gas_inventory, double equat_radius, double gravity)
    {
      equat_radius = KM_EARTH_RADIUS / equat_radius;
      return volatile_gas_inventory * gravity * (EARTH_SURF_PRES_IN_MILLIBARS / 1000.0) / Pow2(equat_radius);
    }

    /// <summary>
    /// This function returns the boiling point of water in an atmosphere of pressure 'surf_pressure', given in millibars. The boiling point is returned in units of Kelvin. This is Fogg's eq. 21.
    /// </summary>
    /// <param name="surf_pressure"></param>
    /// <returns></returns>
    private double boiling_point(double surf_pressure)
    {
      double surface_pressure_in_bars = surf_pressure / MILLIBARS_PER_BAR;
      return 1.0 / ((Math.Log(surface_pressure_in_bars) / -5050.5) + (1.0 / 373.0));
    }

    /// <summary>
    /// This function is Fogg's eq. 22. Given the volatile gas inventory and planetary radius of a planet (in Km), this function returns the fraction of the planet covered with water.
    /// I have changed the function very slightly: the fraction of Earth's surface covered by water is 71%, not 75% as Fogg used.
    /// </summary>
    /// <param name="volatile_gas_inventory"></param>
    /// <param name="planet_radius"></param>
    /// <returns></returns>
    private double hydro_fraction(double volatile_gas_inventory, double planet_radius)
    {
      double temp;
      temp = (0.71 * volatile_gas_inventory / 1000.0)
           * Pow2(KM_EARTH_RADIUS / planet_radius);
      if (temp >= 1.0)
      {
        return 1.0;
      }
      else
      {
        return temp;
      }
    }

    /// <summary>
    /// Given the surface temperature of a planet (in Kelvin), this function returns the fraction of cloud cover available. This is Fogg's eq. 23.
    /// See Hart in "Icarus" (vol 33, pp23 - 39, 1978) for an explanation. This equation is Hart's eq. 3.
    /// I have modified it slightly using constants and relationships from Glass's book "Introduction to Planetary Geology", p. 46.
    /// The 'CLOUD_COVERAGE_FACTOR' is the amount of surface area on Earth covered by one Kg. of cloud.
    /// </summary>
    /// <param name="surf_temp"></param>
    /// <param name="smallest_MW_retained"></param>
    /// <param name="equat_radius"></param>
    /// <param name="hydro_fraction"></param>
    /// <returns></returns>
    private double cloud_fraction(double surf_temp, double smallest_MW_retained, double equat_radius, double hydro_fraction)
    {
      double water_vapor_in_kg, fraction, surf_area, hydro_mass;
      if (smallest_MW_retained > WATER_VAPOR)
      {
        return 0.0;
      }
      else
      {
        surf_area = 4.0 * Math.PI * Pow2(equat_radius);
        hydro_mass = hydro_fraction * surf_area * EARTH_WATER_MASS_PER_AREA;
        water_vapor_in_kg = (0.00000001 * hydro_mass) * Math.Exp(Q2_36 * (surf_temp - EARTH_AVERAGE_KELVIN));
        fraction = CLOUD_COVERAGE_FACTOR * water_vapor_in_kg / surf_area;
        if (fraction >= 1.0)
        {
          return 1.0;
        }
        else
        {
          return fraction;
        }
      }
    }

    /// <summary>
    /// Given the surface temperature of a planet (in Kelvin), this function returns the fraction of the planet's surface covered by ice.
    /// This is Fogg's eq.24.	See Hart[24] in Icarus vol.33, p.28 for an explanation.
    /// I have changed a constant from 70 to 90 in order to bring it more in line with the fraction of the Earth's surface covered with ice, which is approximatly .016 (=1.6%).
    /// </summary>
    /// <param name="hydro_fraction"></param>
    /// <param name="surf_temp"></param>
    /// <returns></returns>
    private double ice_fraction(double hydro_fraction, double surf_temp)
    {
      double temp;
      if (surf_temp > 328.0)
      {
        surf_temp = 328.0;
      }

      temp = Math.Pow(((328.0 - surf_temp) / 90.0), 5.0);
      if (temp > (1.5 * hydro_fraction))
      {
        temp = (1.5 * hydro_fraction);
      }

      if (temp >= 1.0)
      {
        return 1.0;
      }
      else
      {
        return temp;
      }
    }

    /// <summary>
    /// This is Fogg's eq.19. The ecosphere radius is given in AU, the orbital radius in AU, and the temperature returned is in Kelvin.
    /// </summary>
    /// <param name="ecosphere_radius"></param>
    /// <param name="orb_radius"></param>
    /// <param name="albedo"></param>
    /// <returns></returns>
    private double eff_temp(double ecosphere_radius, double orb_radius, double albedo)
    {
      return Math.Sqrt(ecosphere_radius / orb_radius) * Pow1_4((1.0 - albedo) / (1.0 - EARTH_ALBEDO)) * EARTH_EFFECTIVE_TEMP;
    }

    /// <summary>
    /// ???
    /// </summary>
    /// <param name="ecosphere_radius"></param>
    /// <param name="orb_radius"></param>
    /// <param name="albedo"></param>
    /// <returns></returns>
    private double est_temp(double ecosphere_radius, double orb_radius, double albedo)
    {
      return Math.Sqrt(ecosphere_radius / orb_radius) * Pow1_4((1.0 - albedo) / (1.0 - EARTH_ALBEDO)) * EARTH_AVERAGE_KELVIN;
    }

    /// <summary>
    /// Note that if the orbital radius of the planet is greater than or equal to R_inner, 99% of it's volatiles are assumed to have been deposited in surface reservoirs (otherwise, it suffers from the greenhouse effect).
    /// if ((orb_radius is smaller r_greenhouse) AND (zone == 1))
    /// The new definition is based on the inital surface temperature and what state water is in. If it's too hot, the water will never condense out of the atmosphere, rain down and form an ocean. The albedo used here was chosen so that the boundary is about the same as the old method,
    /// Neither zone, nor r_greenhouse are used in this version. JLB
    /// </summary>
    /// <param name="r_ecosphere"></param>
    /// <param name="orb_radius"></param>
    /// <returns></returns>
    private bool grnhouse(double r_ecosphere, double orb_radius)
    {
      double temp = eff_temp(r_ecosphere, orb_radius, GREENHOUSE_TRIGGER_ALBEDO);
      if (temp > FREEZING_POINT_OF_WATER)
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    /// <summary>
    /// This is Fogg's eq.20, and is also Hart's eq.20 in his "Evolution of Earth's Atmosphere" article. The effective temperature given is in units of Kelvin, as is the rise in temperature produced by the greenhouse effect, which is returned.
    /// I tuned this by changing a pow(x,.25) to pow(x,.4) to match Venus - JLB
    /// </summary>
    /// <param name="optical_depth"></param>
    /// <param name="effective_temp"></param>
    /// <param name="surf_pressure"></param>
    /// <returns></returns>
    private double green_rise(double optical_depth, double effective_temp, double surf_pressure)
    {
      double convection_factor = EARTH_CONVECTION_FACTOR * Math.Pow(surf_pressure / EARTH_SURF_PRES_IN_MILLIBARS, 0.4);
      double rise = (Pow1_4(1.0 + 0.75 * optical_depth) - 1.0) * effective_temp * convection_factor;
      if (rise < 0.0)
      {
        rise = 0.0;
      }

      return rise;
    }

    /// <summary>
    /// The surface temperature passed in is in units of Kelvin.
    /// The cloud adjustment is the fraction of cloud cover obscuring each of the three major components of albedo that lie below the clouds.
    /// </summary>
    /// <param name="water_fraction"></param>
    /// <param name="cloud_fraction"></param>
    /// <param name="ice_fraction"></param>
    /// <param name="surf_pressure"></param>
    /// <returns></returns>
    private double planet_albedo(double water_fraction, double cloud_fraction, double ice_fraction, double surf_pressure)
    {
      double rock_fraction, cloud_adjustment, components, cloud_part, rock_part, water_part, ice_part;

      rock_fraction = 1.0 - water_fraction - ice_fraction;
      components = 0.0;
      if (water_fraction > 0.0)
      {
        components = components + 1.0;
      }

      if (ice_fraction > 0.0)
      {
        components = components + 1.0;
      }

      if (rock_fraction > 0.0)
      {
        components = components + 1.0;
      }

      cloud_adjustment = cloud_fraction / components;
      if (rock_fraction >= cloud_adjustment)
      {
        rock_fraction = rock_fraction - cloud_adjustment;
      }
      else
      {
        rock_fraction = 0.0;
      }

      if (water_fraction > cloud_adjustment)
      {
        water_fraction = water_fraction - cloud_adjustment;
      }
      else
      {
        water_fraction = 0.0;
      }

      if (ice_fraction > cloud_adjustment)
      {
        ice_fraction = ice_fraction - cloud_adjustment;
      }
      else
      {
        ice_fraction = 0.0;
      }

      cloud_part = cloud_fraction * CLOUD_ALBEDO;   /* about(...,0.2); */
      if (surf_pressure == 0.0)
      {
        rock_part = rock_fraction * ROCKY_AIRLESS_ALBEDO; /* about(...,0.3); */
        ice_part = ice_fraction * AIRLESS_ICE_ALBEDO;   /* about(...,0.4); */
        water_part = 0;
      }
      else
      {
        rock_part = rock_fraction * ROCKY_ALBEDO; /* about(...,0.1); */
        water_part = water_fraction * WATER_ALBEDO; /* about(...,0.2); */
        ice_part = ice_fraction * ICE_ALBEDO;   /* about(...,0.1); */
      }
      return cloud_part + rock_part + water_part + ice_part;
    }

    /// <summary>
    /// This function returns the dimensionless quantity of optical depth, which is useful in determining the amount of greenhouse effect on planet.
    /// </summary>
    /// <param name="molecular_weight"></param>
    /// <param name="surf_pressure"></param>
    /// <returns></returns>
    private double opacity(double molecular_weight, double surf_pressure)
    {
      double optical_depth;
      optical_depth = 0.0;
      if ((molecular_weight >= 0.0) && (molecular_weight < 10.0))
      {
        optical_depth = optical_depth + 3.0;
      }

      if ((molecular_weight >= 10.0) && (molecular_weight < 20.0))
      {
        optical_depth = optical_depth + 2.34;
      }

      if ((molecular_weight >= 20.0) && (molecular_weight < 30.0))
      {
        optical_depth = optical_depth + 1.0;
      }

      if ((molecular_weight >= 30.0) && (molecular_weight < 45.0))
      {
        optical_depth = optical_depth + 0.15;
      }

      if ((molecular_weight >= 45.0) && (molecular_weight < 100.0))
      {
        optical_depth = optical_depth + 0.05;
      }

      if (surf_pressure >= (70.0 * EARTH_SURF_PRES_IN_MILLIBARS))
      {
        optical_depth = optical_depth * 8.333;
      }
      else if (surf_pressure >= (50.0 * EARTH_SURF_PRES_IN_MILLIBARS))
      {
        optical_depth = optical_depth * 6.666;
      }
      else if (surf_pressure >= (30.0 * EARTH_SURF_PRES_IN_MILLIBARS))
      {
        optical_depth = optical_depth * 3.333;
      }
      else if (surf_pressure >= (10.0 * EARTH_SURF_PRES_IN_MILLIBARS))
      {
        optical_depth = optical_depth * 2.0;
      }
      else if (surf_pressure >= (5.0 * EARTH_SURF_PRES_IN_MILLIBARS))
      {
        optical_depth = optical_depth * 1.5;
      }

      return optical_depth;
    }

    /// <summary>
    /// It calculates the number of years it takes for 1/e of a gas to escape from a planet's atmosphere.
    /// Taken from Dole p. 34. He cites Jeans (1916) and Jones (1923).
    /// </summary>
    /// <param name="molecular_weight"></param>
    /// <param name="planet"></param>
    /// <returns></returns>
    private double gas_life(double molecular_weight, planet_record planet)
    {
      double v = rms_vel(molecular_weight, planet.exospheric_temp);
      double g = planet.surf_grav * EARTH_ACCELERATION;
      double r = (planet.radius * CM_PER_KM);
      double t = (Pow3(v) / (2.0 * Pow2(g) * r)) * Math.Exp((3.0 * g * r) / Pow2(v));
      double years = t / (SECONDS_PER_HOUR * 24.0 * DAYS_IN_A_YEAR);

      //	double ve = planet.esc_velocity;
      //	double k = 2;
      //	double t2 = ((k * Pow3(v) * r) / Pow4(ve)) * Math.Exp((3.0 * Pow2(ve)) / (2.0 * Pow2(v)));
      //	double years2 = t2 / (SECONDS_PER_HOUR * 24.0 * DAYS_IN_A_YEAR);

      //	if (flag_verbose & 0x0040)
      //		fprintf (stderr, "gas_life: %LGs, V ratio: %Lf\n", 
      //				years, ve / v);

      if (years > 2.0E10)
      {
        years = INCREDIBLY_LARGE_NUMBER;
      }

      return years;
    }

    /// <summary>
    /// ???
    /// </summary>
    /// <param name="planet"></param>
    /// <returns></returns>
    private double min_molec_weight(planet_record planet)
    {
      double mass = planet.mass;
      double radius = planet.radius;
      double temp = planet.exospheric_temp;
      double target = 5.0E9;
      double guess_1 = molecule_limit(mass, radius, temp);
      double guess_2 = guess_1;
      double life = gas_life(guess_1, planet);
      int loops = 0;
      // if (planet.sun != null) target = planet->sun->age;
      target = planet.sun.age;
      if (life > target)
      {
        while ((life > target) && (loops++ < 25))
        {
          guess_1 = guess_1 / 2.0;
          life = gas_life(guess_1, planet);
        }
      }
      else
      {
        while ((life < target) && (loops++ < 25))
        {
          guess_2 = guess_2 * 2.0;
          life = gas_life(guess_2, planet);
        }
      }
      loops = 0;
      while (((guess_2 - guess_1) > 0.1) && (loops++ < 25))
      {
        double guess_3 = (guess_1 + guess_2) / 2.0;
        life = gas_life(guess_3, planet);
        if (life < target)
        {
          guess_1 = guess_3;
        }
        else
        {
          guess_2 = guess_3;
        }
      }
      life = gas_life(guess_2, planet);
      return guess_2;
    }

    /// <summary>
    /// Inspired partial pressure, taking into account humidification of the air in the nasal passage and throat. This formula is on Dole's p. 14.
    /// </summary>
    /// <param name="surf_pressure"></param>
    /// <param name="gas_pressure"></param>
    /// <returns></returns>
    private double inspired_partial_pressure(double surf_pressure, double gas_pressure)
    {
      double pH2O = (H20_ASSUMED_PRESSURE);
      double fraction = gas_pressure / surf_pressure;
      return (surf_pressure - pH2O) * fraction;
    }

    /// <summary>
    /// This function uses figures on the maximum inspired partial pressures of Oxygen, other atmospheric and traces gases as laid out on pages 15, 16 and 18 of Dole's Habitable Planets for Man to derive breathability of the planet's atmosphere. - JLB
    /// </summary>
    /// <param name="planet"></param>
    /// <returns></returns>
    private uint breathability(planet_record planet)
    {
      bool oxygen_ok = false;
      int index;
      if (planet.gases == 0)
      {
        return NONE;
      }

      for (index = 0; index < planet.gases; index++)
      {
        int n;
        int gas_no = 0;
        double ipp = inspired_partial_pressure(planet.surf_pressure, planet.atmosphere.surf_pressure);
        for (n = 0; n < max_gas; n++)
        {
          if (gases[n].num == planet.atmosphere.num)
          {
            gas_no = n;
          }
        }
        if (ipp > gases[gas_no].max_ipp)
        {
          return POISONOUS;
        }

        if (planet.atmosphere.num == AN_O)
        {
          oxygen_ok = ((ipp >= MIN_O2_IPP) && (ipp <= MAX_O2_IPP));
        }
      }
      if (oxygen_ok)
      {
        return BREATHABLE;
      }
      else
      {
        return UNBREATHABLE;
      }
    }

    /// <summary>
    /// function for 'soft limiting' temperatures
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    private double lim(double x)
    {
      return x / Math.Sqrt(Math.Sqrt(1 + x * x * x * x));
    }

    /// <summary>
    /// ???
    /// </summary>
    /// <param name="v"></param>
    /// <param name="max"></param>
    /// <param name="min"></param>
    /// <returns></returns>
    private double soft(double v, double max, double min)
    {
      double dv = v - min;
      double dm = max - min;
      return (lim(2 * dv / dm - 1) + 1) / 2 * dm + min;
    }

    /// <summary>
    /// ???
    /// </summary>
    /// <param name="planet"></param>
    private void set_temp_range(planet_record planet)
    {
      double pressmod = 1 / Math.Sqrt(1 + 20 * planet.surf_pressure / 1000.0);
      double ppmod = 1 / Math.Sqrt(10 + 5 * planet.surf_pressure / 1000.0);
      double tiltmod = Math.Abs(Math.Cos(planet.axial_tilt * Math.PI / 180) * Math.Pow(1 + planet.e, 2));
      double daymod = 1 / (200 / planet.day + 1);
      double mh = Math.Pow(1 + daymod, pressmod);
      double ml = Math.Pow(1 - daymod, pressmod);
      double hi = mh * planet.surf_temp;
      double lo = ml * planet.surf_temp;
      double sh = hi + Math.Pow((100 + hi) * tiltmod, Math.Sqrt(ppmod));
      double wl = lo - Math.Pow((150 + lo) * tiltmod, Math.Sqrt(ppmod));
      double max = planet.surf_temp + Math.Sqrt(planet.surf_temp) * 10;
      double min = planet.surf_temp / Math.Sqrt(planet.day + 24);
      if (lo < min)
      {
        lo = min;
      }

      if (wl < 0)
      {
        wl = 0;
      }

      planet.high_temp = soft(hi, max, min);
      planet.low_temp = soft(lo, max, min);
      planet.max_temp = soft(sh, max, min);
      planet.min_temp = soft(wl, max, min);
    }

    /// <summary>
    /// The temperature calculated is in degrees Kelvin.
    /// Quantities already known which are used in these calculations:
    ///  * planet->molec_weight
    ///  * planet->surf_pressure
    ///  * R_ecosphere
    ///  * planet->a
    ///  * planet->volatile_gas_inventory
    ///  * planet->radius
    ///  * planet->boil_point
    /// </summary>
    /// <param name="planet"></param>
    /// <param name="first"></param>
    /// <param name="last_clouds"></param>
    /// <param name="last_ice"></param>
    /// <param name="last_temp"></param>
    /// <param name="last_albedo"></param>
    private void calculate_surface_temp(planet_record planet, bool first, double last_water, double last_clouds, double last_ice, double last_temp, double last_albedo)
    {
      double effective_temp;
      double water_raw;
      double clouds_raw;
      double greenhouse_temp;
      bool boil_off = false;
      if (first)
      {
        planet.albedo = EARTH_ALBEDO;
        effective_temp = eff_temp(planet.sun.r_ecosphere, planet.a, planet.albedo);
        greenhouse_temp = green_rise(opacity(planet.molec_weight, planet.surf_pressure), effective_temp, planet.surf_pressure);
        planet.surf_temp = effective_temp + greenhouse_temp;
        set_temp_range(planet);
      }
      if ((planet.greenhouse_effect) && (planet.max_temp < planet.boil_point))
      {
        /*if (flag_verbose & 0x0010)
          fprintf(stderr, "Deluge: %s %d max (%Lf) < boil (%Lf)\n",
              planet.sun.name,
              planet.planet_no,
              planet.max_temp,
              planet.boil_point);*/
        planet.greenhouse_effect = false;
        planet.volatile_gas_inventory = vol_inventory(planet.mass, planet.esc_velocity, planet.rms_velocity, planet.sun.mass, planet.orbit_zone, planet.greenhouse_effect, (planet.gas_mass / planet.mass) > 0.000001);
        planet.surf_pressure = pressure(planet.volatile_gas_inventory, planet.radius, planet.surf_grav);
        planet.boil_point = boiling_point(planet.surf_pressure);
      }
      water_raw = planet.hydrosphere = hydro_fraction(planet.volatile_gas_inventory, planet.radius);
      clouds_raw = planet.cloud_cover = cloud_fraction(planet.surf_temp, planet.molec_weight, planet.radius, planet.hydrosphere);
      planet.ice_cover = ice_fraction(planet.hydrosphere, planet.surf_temp);
      if ((planet.greenhouse_effect) && (planet.surf_pressure > 0.0))
      {
        planet.cloud_cover = 1.0;
      }

      if ((planet.high_temp >= planet.boil_point) && (!first) && !((int)planet.day == (int)(planet.orb_period * 24.0) || (planet.resonant_period)))
      {
        planet.hydrosphere = 0.0;
        boil_off = true;
        if (planet.molec_weight > WATER_VAPOR)
        {
          planet.cloud_cover = 0.0;
        }
        else
        {
          planet.cloud_cover = 1.0;
        }
      }
      if (planet.surf_temp < (FREEZING_POINT_OF_WATER - 3.0))
      {
        planet.hydrosphere = 0.0;
      }

      planet.albedo = planet_albedo(planet.hydrosphere, planet.cloud_cover, planet.ice_cover, planet.surf_pressure);
      effective_temp = eff_temp(planet.sun.r_ecosphere, planet.a, planet.albedo);
      greenhouse_temp = green_rise(opacity(planet.molec_weight, planet.surf_pressure), effective_temp, planet.surf_pressure);
      planet.surf_temp = effective_temp + greenhouse_temp;
      if (!first)
      {
        if (!boil_off)
        {
          planet.hydrosphere = (planet.hydrosphere + (last_water * 2)) / 3;
        }

        planet.cloud_cover = (planet.cloud_cover + (last_clouds * 2)) / 3;
        planet.ice_cover = (planet.ice_cover + (last_ice * 2)) / 3;
        planet.albedo = (planet.albedo + (last_albedo * 2)) / 3;
        planet.surf_temp = (planet.surf_temp + (last_temp * 2)) / 3;
      }
      set_temp_range(planet);
      /*if (flag_verbose & 0x0020)
        fprintf(stderr, "%5.1Lf AU: %5.1Lf = %5.1Lf ef + %5.1Lf gh%c "
            "(W: %4.2Lf (%4.2Lf) C: %4.2Lf (%4.2Lf) I: %4.2Lf A: (%4.2Lf))\n",
            planet.a,
            planet.surf_temp - FREEZING_POINT_OF_WATER,
            effective_temp - FREEZING_POINT_OF_WATER,
            greenhouse_temp,
            (planet.greenhouse_effect) ? '*' : ' ',
            planet.hydrosphere, water_raw,
            planet.cloud_cover, clouds_raw,
            planet.ice_cover,
            planet.albedo);*/
    }

    private void iterate_surface_temp(planet_record planet)
    {
      int count = 0;
      double initial_temp = est_temp(planet.sun.r_ecosphere, planet.a, planet.albedo);
      double h2_life = gas_life(MOL_HYDROGEN, planet);
      double h2o_life = gas_life(WATER_VAPOR, planet);
      double n2_life = gas_life(MOL_NITROGEN, planet);
      double n_life = gas_life(ATOMIC_NITROGEN, planet);
      /*if (flag_verbose & 0x20000)
        fprintf(stderr, "%d:                     %5.1Lf it [%5.1Lf re %5.1Lf a %5.1Lf alb]\n",
            planet.planet_no,
            initial_temp,
            planet.sun.r_ecosphere, planet.a, planet.albedo
            );*/
      /*if (flag_verbose & 0x0040)
        fprintf(stderr, "\nGas lifetimes: H2 - %Lf, H2O - %Lf, N - %Lf, N2 - %Lf\n",
            h2_life, h2o_life, n_life, n2_life);*/
      calculate_surface_temp(planet, true, 0, 0, 0, 0, 0);
      for (count = 0; count <= 25; count++)
      {
        double last_water = planet.hydrosphere;
        double last_clouds = planet.cloud_cover;
        double last_ice = planet.ice_cover;
        double last_temp = planet.surf_temp;
        double last_albedo = planet.albedo;
        calculate_surface_temp(planet, false, last_water, last_clouds, last_ice, last_temp, last_albedo);
        if (Math.Abs(planet.surf_temp - last_temp) < 0.25)
        {
          break;
        }
      }
      planet.greenhs_rise = planet.surf_temp - initial_temp;
      /*if (flag_verbose & 0x20000)
        fprintf(stderr, "%d: %5.1Lf gh = %5.1Lf (%5.1Lf C) st - %5.1Lf it [%5.1Lf re %5.1Lf a %5.1Lf alb]\n",
            planet.planet_no,
            planet.greenhs_rise,
            planet.surf_temp,
            planet.surf_temp - FREEZING_POINT_OF_WATER,
            initial_temp,
            planet.sun.r_ecosphere, planet.a, planet.albedo
            );*/
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="inner_limit_of_dust"></param>
    /// <param name="outer_limit_of_dust"></param>
    private void set_initial_conditions(double inner_limit_of_dust, double outer_limit_of_dust)
    {
      gen hist;
      hist.dusts = dust_head;
      hist.planets = planet_head;
      //hist.next = hist_head;
      hist_head = hist;
      //planet_head = NULL;
      //dust_head.next_band = NULL;
      dust_head.outer_edge = outer_limit_of_dust;
      dust_head.inner_edge = inner_limit_of_dust;
      dust_head.dust_present = true;
      dust_head.gas_present = true;
      dust_left = true;
      cloud_eccentricity = 0.2;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="stell_mass_ratio"></param>
    /// <returns></returns>
    private double stellar_dust_limit(double stell_mass_ratio)
    {
      return (200.0 * Math.Pow(stell_mass_ratio, (1.0 / 3.0)));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="stell_mass_ratio"></param>
    /// <returns></returns>
    private double nearest_planet(double stell_mass_ratio)
    {
      return (0.3 * Math.Pow(stell_mass_ratio, (1.0 / 3.0)));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="stell_mass_ratio"></param>
    /// <returns></returns>
    private double farthest_planet(double stell_mass_ratio)
    {
      return (50.0 * Math.Pow(stell_mass_ratio, (1.0 / 3.0)));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="a"></param>
    /// <param name="e"></param>
    /// <param name="mass"></param>
    /// <returns></returns>
    private double inner_effect_limit(double a, double e, double mass)
    {
      return (a * (1.0 - e) * (1.0 - mass) / (1.0 + cloud_eccentricity));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="a"></param>
    /// <param name="e"></param>
    /// <param name="mass"></param>
    /// <returns></returns>
    private double outer_effect_limit(double a, double e, double mass)
    {
      return (a * (1.0 + e) * (1.0 + mass) / (1.0 - cloud_eccentricity));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="inside_range"></param>
    /// <param name="outside_range"></param>
    /// <returns></returns>
    private bool dust_available(double inside_range, double outside_range)
    {
      dust_record current_dust_band;
      bool dust_here = false;
      current_dust_band = dust_head;
      /*
      while ((current_dust_band != NULL)
        && (current_dust_band.outer_edge < inside_range))
        current_dust_band = current_dust_band.next_band;
      if (current_dust_band == NULL)
        dust_here = true;
      else dust_here = current_dust_band.dust_present;
      while ((current_dust_band != NULL)
        && (current_dust_band.inner_edge < outside_range))
      {
        dust_here = dust_here || current_dust_band.dust_present;
        current_dust_band = current_dust_band.next_band;
      }
      */
      return (dust_here);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <param name="mass"></param>
    /// <param name="crit_mass"></param>
    /// <param name="body_inner_bound"></param>
    /// <param name="body_outer_bound"></param>
    private void update_dust_lanes(double min, double max, double mass, double crit_mass, double body_inner_bound, double body_outer_bound)
    {
      bool gas;
      dust_record node1;
      dust_record node2;
      dust_record node3;

      dust_left = false;
      if ((mass > crit_mass))
      {
        gas = false;
      }
      else
      {
        gas = true;
      }

      node1 = dust_head;
      /*
      while ((node1 != NULL))
      {
        if (((node1.inner_edge < min) && (node1.outer_edge > max)))
        {
          node2 = (dust*)malloc(sizeof(dust));
          node2.inner_edge = min;
          node2.outer_edge = max;
          if ((node1.gas_present == true))
            node2.gas_present = gas;
          else
            node2.gas_present = false;
          node2.dust_present = false;
          node3 = (dust*)malloc(sizeof(dust));
          node3.inner_edge = max;
          node3.outer_edge = node1.outer_edge;
          node3.gas_present = node1.gas_present;
          node3.dust_present = node1.dust_present;
          node3.next_band = node1.next_band;
          node1.next_band = node2;
          node2.next_band = node3;
          node1.outer_edge = min;
          node1 = node3.next_band;
        }
        else
          if (((node1.inner_edge < max) && (node1.outer_edge > max)))
        {
          node2 = (dust*)malloc(sizeof(dust));
          node2.next_band = node1.next_band;
          node2.dust_present = node1.dust_present;
          node2.gas_present = node1.gas_present;
          node2.outer_edge = node1.outer_edge;
          node2.inner_edge = max;
          node1.next_band = node2;
          node1.outer_edge = max;
          if ((node1.gas_present == true))
            node1.gas_present = gas;
          else
            node1.gas_present = false;
          node1.dust_present = false;
          node1 = node2.next_band;
        }
        else
            if (((node1.inner_edge < min) && (node1.outer_edge > min)))
        {
          node2 = (dust*)malloc(sizeof(dust));
          node2.next_band = node1.next_band;
          node2.dust_present = false;
          if ((node1.gas_present == true))
            node2.gas_present = gas;
          else
            node2.gas_present = false;
          node2.outer_edge = node1.outer_edge;
          node2.inner_edge = min;
          node1.next_band = node2;
          node1.outer_edge = min;
          node1 = node2.next_band;
        }
        else
              if (((node1.inner_edge >= min) && (node1.outer_edge <= max)))
        {
          if ((node1.gas_present == true))
            node1.gas_present = gas;
          node1.dust_present = false;
          node1 = node1.next_band;
        }
        else
                if (((node1.outer_edge < min) || (node1.inner_edge > max)))
          node1 = node1.next_band;
      }
      node1 = dust_head;
      while ((node1 != NULL))
      {
        if (((node1.dust_present)
          && (((node1.outer_edge >= body_inner_bound)
            && (node1.inner_edge <= body_outer_bound)))))
          dust_left = true;
        node2 = node1.next_band;
        if ((node2 != NULL))
        {
          if (((node1.dust_present == node2.dust_present)
            && (node1.gas_present == node2.gas_present)))
          {
            node1.outer_edge = node2.outer_edge;
            node1.next_band = node2.next_band;
            free(node2);
          }
        }
        node1 = node1.next_band;
      }
      */
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="last_mass"></param>
    /// <param name="new_dust"></param>
    /// <param name="new_gas"></param>
    /// <param name="a"></param>
    /// <param name="e"></param>
    /// <param name="crit_mass"></param>
    /// <param name="dust_band"></param>
    /// <returns></returns>
    private double collect_dust(double last_mass, double new_dust, double new_gas, double a, double e, double crit_mass, dust_record dust_band)
    {
      double mass_density;
      double temp1;
      double temp2;
      double temp;
      double temp_density;
      double bandwidth;
      double width;
      double volume;
      double gas_density = 0.0;
      double new_mass;
      double next_mass;
      double next_dust = 0;
      double next_gas = 0;
      temp = last_mass / (1.0 + last_mass);
      reduced_mass = Math.Pow(temp, (1.0 / 4.0));
      r_inner = inner_effect_limit(a, e, reduced_mass);
      r_outer = outer_effect_limit(a, e, reduced_mass);
      if (r_inner < 0.0)
      {
        r_inner = 0.0;
      }

      /*
      if ((dust_band == NULL))
        return (0.0);
      else
      {
        if ((dust_band.dust_present == false))
          temp_density = 0.0;
        else
          temp_density = dust_density;

        if (((last_mass < crit_mass) || (dust_band.gas_present == false)))
          mass_density = temp_density;
        else
        {
          mass_density = K * temp_density / (1.0 + Math.Srt(crit_mass / last_mass)
                        * (K - 1.0));
          gas_density = mass_density - temp_density;
        }

        if (((dust_band.outer_edge <= r_inner)
          || (dust_band.inner_edge >= r_outer)))
        {
          return (collect_dust(last_mass, new_dust, new_gas,
                    a, e, crit_mass, dust_band.next_band));
        }
        else
        {
          bandwidth = (r_outer - r_inner);

          temp1 = r_outer - dust_band.outer_edge;
          if (temp1 < 0.0)
            temp1 = 0.0;
          width = bandwidth - temp1;

          temp2 = dust_band.inner_edge - r_inner;
          if (temp2 < 0.0)
            temp2 = 0.0;
          width = width - temp2;

          temp = 4.0 * Math.PI * Math.Pow(a, 2.0) * reduced_mass
            * (1.0 - e * (temp1 - temp2) / bandwidth);
          volume = temp * width;

          new_mass = volume * mass_density;
          new_gas = volume * gas_density;
          new_dust = new_mass - new_gas;

          next_mass = collect_dust(last_mass, next_dust, next_gas,
                       a, e, crit_mass, dust_band.next_band);

          new_gas = new_gas + next_gas;
          new_dust = new_dust + next_dust;

          return (new_mass + next_mass);
        }
      }*/

      return 0; // später lösen
    }

    /// <summary>
    /// Orbital radius is in AU, eccentricity is unitless, and the stellar luminosity ratio is with respect to the sun. The value returned is the mass at which the planet begins to accrete gas as well as dust, and is in units of solar masses.
    /// </summary>
    /// <param name="orb_radius"></param>
    /// <param name="eccentricity"></param>
    /// <param name="stell_luminosity_ratio"></param>
    /// <returns></returns>
    private double critical_limit(double orb_radius, double eccentricity, double stell_luminosity_ratio)
    {
      double temp;
      double perihelion_dist;
      perihelion_dist = (orb_radius - orb_radius * eccentricity);
      temp = perihelion_dist * Math.Sqrt(stell_luminosity_ratio);
      return (B * Math.Pow(temp, -0.75));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="seed_mass"></param>
    /// <param name="new_dust"></param>
    /// <param name="new_gas"></param>
    /// <param name="a"></param>
    /// <param name="e"></param>
    /// <param name="crit_mass"></param>
    /// <param name="body_inner_bound"></param>
    /// <param name="body_outer_bound"></param>
    private void accrete_dust(double seed_mass, double new_dust, double new_gas, double a, double e, double crit_mass, double body_inner_bound, double body_outer_bound)
    {
      double new_mass = (seed_mass);
      double temp_mass;
      do
      {
        temp_mass = new_mass;
        new_mass = collect_dust(new_mass, new_dust, new_gas,
                    a, e, crit_mass, dust_head);
      } while (!(((new_mass - temp_mass) < (0.0001 * temp_mass))));
      (seed_mass) = (seed_mass) + new_mass;
      update_dust_lanes(r_inner, r_outer, (seed_mass), crit_mass, body_inner_bound, body_outer_bound);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="a"></param>
    /// <param name="e"></param>
    /// <param name="mass"></param>
    /// <param name="crit_mass"></param>
    /// <param name="dust_mass"></param>
    /// <param name="gas_mass"></param>
    /// <param name="stell_luminosity_ratio"></param>
    /// <param name="body_inner_bound"></param>
    /// <param name="body_outer_bound"></param>
    /// <param name="do_moons"></param>
    private void coalesce_planetesimals(double a, double e, double mass, double crit_mass, double dust_mass, double gas_mass, double stell_luminosity_ratio, double body_inner_bound, double body_outer_bound, bool do_moons)
    {
      planet_record the_planet;
      planet_record next_planet;
      planet_record prev_planet;
      bool finished = false;
      double temp;
      double diff;
      double dist1;
      double dist2;
      //prev_planet = NULL;

      // First we try to find an existing planet with an over-lapping orbit.

      /*for (the_planet = planet_head; the_planet != NULL; the_planet = the_planet.next_planet)
      {
        diff = the_planet.a - a;
        if ((diff > 0.0))
        {
          dist1 = (a * (1.0 + e) * (1.0 + reduced_mass)) - a;
          // x aphelion
          reduced_mass = Math.Pow((the_planet.mass / (1.0 + the_planet.mass)), (1.0 / 4.0));
          dist2 = the_planet.a
            - (the_planet.a * (1.0 - the_planet.e) * (1.0 - reduced_mass));
        }
        else
        {
          dist1 = a - (a * (1.0 - e) * (1.0 - reduced_mass));
          // x perihelion
          reduced_mass = Math.Pow((the_planet.mass / (1.0 + the_planet.mass)), (1.0 / 4.0));
          dist2 = (the_planet.a * (1.0 + the_planet.e) * (1.0 + reduced_mass))
            - the_planet.a;
        }

        if (((Math.Abs(diff) <= Math.Abs(dist1)) || (Math.Abs(diff) <= Math.Abs(dist2))))
        {
          double new_dust = 0;
          double new_gas = 0;
          double new_a = (the_planet.mass + mass) /
                    ((the_planet.mass / the_planet.a) + (mass / a));

          temp = the_planet.mass * Math.Sqrt(the_planet.a) * Math.Sqrt(1.0 - Math.Pow(the_planet.e, 2.0));
          temp = temp + (mass * Math.Sqrt(a) * Math.Sqrt(Math.Sqrt(1.0 - Math.Pow(e, 2.0))));
          temp = temp / ((the_planet.mass + mass) * Math.Sqrt(new_a));
          temp = 1.0 - Math.Pow(temp, 2.0);
          if (((temp < 0.0) || (temp >= 1.0)))
            temp = 0.0;
          e = Math.Sqrt(temp);

          if (do_moons)
          {
            double existing_mass = 0.0;

            if (the_planet.first_moon != NULL)
            {
              planet_record m;

              for (m = the_planet.first_moon;
                 m != NULL;
                 m = m.next_planet)
              {
                existing_mass += m.mass;
              }
            }

            if (mass < crit_mass)
            {
              if ((mass * SUN_MASS_IN_EARTH_MASSES) < 2.5
               && (mass * SUN_MASS_IN_EARTH_MASSES) > .0001
               && existing_mass < (the_planet.mass * .05)
                 )
              {
                planet_record the_moon = (planets*)malloc(sizeof(planets));

                the_moon.type = planet_type.tUnknown;
                // 					the_moon.a 			= a;
                // 					the_moon.e 			= e;
                the_moon.mass = mass;
                the_moon.dust_mass = dust_mass;
                the_moon.gas_mass = gas_mass;
                the_moon.atmosphere = NULL;
                the_moon.next_planet = NULL;
                the_moon.first_moon = NULL;
                the_moon.gas_giant = false;
                the_moon.atmosphere = NULL;
                the_moon.albedo = 0;
                the_moon.gases = 0;
                the_moon.surf_temp = 0;
                the_moon.high_temp = 0;
                the_moon.low_temp = 0;
                the_moon.max_temp = 0;
                the_moon.min_temp = 0;
                the_moon.greenhs_rise = 0;
                the_moon.minor_moons = 0;

                if ((the_moon.dust_mass + the_moon.gas_mass)
                  > (the_planet.dust_mass + the_planet.gas_mass))
                {
                  double temp_dust = the_planet.dust_mass;
                  double temp_gas = the_planet.gas_mass;
                  double temp_mass = the_planet.mass;

                  the_planet.dust_mass = the_moon.dust_mass;
                  the_planet.gas_mass = the_moon.gas_mass;
                  the_planet.mass = the_moon.mass;

                  the_moon.dust_mass = temp_dust;
                  the_moon.gas_mass = temp_gas;
                  the_moon.mass = temp_mass;
                }

                if (the_planet.first_moon == NULL)
                  the_planet.first_moon = the_moon;
                else
                {
                  the_moon.next_planet = the_planet.first_moon;
                  the_planet.first_moon = the_moon;
                }

                finished = true;

                if (flag_verbose & 0x0100)
                  fprintf(stderr, "Moon Captured... "
                       "%5.3Lf AU (%.2LfEM) <- %.2LfEM\n",
                      the_planet.a, the_planet.mass * SUN_MASS_IN_EARTH_MASSES,
                      mass * SUN_MASS_IN_EARTH_MASSES
                      );
              }
              else
              {
                if (flag_verbose & 0x0100)
                  fprintf(stderr, "Moon Escapes... "
                       "%5.3Lf AU (%.2LfEM)%s %.2LfEM%s\n",
                      the_planet.a, the_planet.mass * SUN_MASS_IN_EARTH_MASSES,
                      existing_mass < (the_planet.mass * .05) ? "" : " (big moons)",
                      mass * SUN_MASS_IN_EARTH_MASSES,
                      (mass * SUN_MASS_IN_EARTH_MASSES) >= 2.5 ? ", too big" :
                        (mass * SUN_MASS_IN_EARTH_MASSES) <= .0001 ? ", too small" : ""
                      );
              }
            }
          }*/

      /*if (!finished)
      {
        if (flag_verbose & 0x0100)
          fprintf(stderr, "Collision between two planetesimals! "
              "%4.2Lf AU (%.2LfEM) + %4.2Lf AU (%.2LfEM = %.2LfEMd + %.2LfEMg [%.3LfEM]). %5.3Lf AU (%5.3Lf)\n",
              the_planet.a, the_planet.mass * SUN_MASS_IN_EARTH_MASSES,
              a, mass * SUN_MASS_IN_EARTH_MASSES,
              dust_mass * SUN_MASS_IN_EARTH_MASSES, gas_mass * SUN_MASS_IN_EARTH_MASSES,
              crit_mass * SUN_MASS_IN_EARTH_MASSES,
              new_a, e);

        temp = the_planet.mass + mass;
        accrete_dust(temp, new_dust, new_gas,
               new_a, e, stell_luminosity_ratio,
               body_inner_bound, body_outer_bound);

        the_planet.a = new_a;
        the_planet.e = e;
        the_planet.mass = temp;
        the_planet.dust_mass += dust_mass + new_dust;
        the_planet.gas_mass += gas_mass + new_gas;
        if (temp >= crit_mass)
          the_planet.gas_giant = true;

        while (the_planet.next_planet != NULL && the_planet.next_planet.a < new_a)
        {
          next_planet = the_planet.next_planet;

          if (the_planet == planet_head)
            planet_head = next_planet;
          else
            prev_planet.next_planet = next_planet;

          the_planet.next_planet = next_planet.next_planet;
          next_planet.next_planet = the_planet;
          prev_planet = next_planet;
        }
      }

      finished = true;
      break;
    }
    else
    {
      prev_planet = the_planet;
    }
  }*/

      /*if (!(finished))      // Planetesimals didn't collide. Make it a planet.
      {
        the_planet = (planets*)malloc(sizeof(planets));

        the_planet.type = tUnknown;
        the_planet.a = a;
        the_planet.e = e;
        the_planet.mass = mass;
        the_planet.dust_mass = dust_mass;
        the_planet.gas_mass = gas_mass;
        the_planet.atmosphere = NULL;
        the_planet.first_moon = NULL;
        the_planet.atmosphere = NULL;
        the_planet.albedo = 0;
        the_planet.gases = 0;
        the_planet.surf_temp = 0;
        the_planet.high_temp = 0;
        the_planet.low_temp = 0;
        the_planet.max_temp = 0;
        the_planet.min_temp = 0;
        the_planet.greenhs_rise = 0;
        the_planet.minor_moons = 0;

        if ((mass >= crit_mass))
          the_planet.gas_giant = true;
        else
          the_planet.gas_giant = false;

        if ((planet_head == NULL))
        {
          planet_head = the_planet;
          the_planet.next_planet = NULL;
        }
        else if ((a < planet_head.a))
        {
          the_planet.next_planet = planet_head;
          planet_head = the_planet;
        }
        else if ((planet_head.next_planet == NULL))
        {
          planet_head.next_planet = the_planet;
          the_planet.next_planet = NULL;
        }
        else
        {
          next_planet = planet_head;
          while (((next_planet != NULL) && (next_planet.a < a)))
          {
            prev_planet = next_planet;
            next_planet = next_planet.next_planet;
          }
          the_planet.next_planet = next_planet;
          prev_planet.next_planet = the_planet;
        }
      }*/
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="stell_mass_ratio"></param>
    /// <param name="stell_luminosity_ratio"></param>
    /// <param name="inner_dust"></param>
    /// <param name="outer_dust"></param>
    /// <param name="outer_planet_limit"></param>
    /// <param name="dust_density_coeff"></param>
    /// <param name="seed_system"></param>
    /// <param name="do_moons"></param>
    /// <returns></returns>
    private planet_record dist_planetary_masses(double stell_mass_ratio, double stell_luminosity_ratio, double inner_dust, double outer_dust, double outer_planet_limit, double dust_density_coeff, planet_record seed_system, bool do_moons)
    {
      double a;
      double e;
      double mass;
      double dust_mass;
      double gas_mass;
      double crit_mass;
      double planet_inner_bound;
      double planet_outer_bound;
      planet_record seeds = seed_system;

      set_initial_conditions(inner_dust, outer_dust);
      planet_inner_bound = nearest_planet(stell_mass_ratio);

      if (outer_planet_limit == 0)
      {
        planet_outer_bound = farthest_planet(stell_mass_ratio);
      }
      else
      {
        planet_outer_bound = outer_planet_limit;
      }

      /*
      while (dust_left)
       {
         if (seeds != NULL)
         {
           a = seeds.a;
           e = seeds.e;
           seeds = seeds.next_planet;
         }
         else
         {
           a = randomNumber(planet_inner_bound, planet_outer_bound);
           e = randomEccentricity();
         }

         mass = PROTOPLANET_MASS;
         dust_mass = 0;
         gas_mass = 0;

         if (flag_verbose & 0x0200)
           fprintf(stderr, "Checking %Lg AU.\n", a);

         if (dust_available(inner_effect_limit(a, e, mass),
                    outer_effect_limit(a, e, mass)))
         {
           if (flag_verbose & 0x0100)
             fprintf(stderr, "Injecting protoplanet at %Lg AU.\n", a);

           dust_density = dust_density_coeff * Math.Sqrt(stell_mass_ratio)
                    * Math.Exp(-ALPHA * Math.Pow(a, (1.0 / N)));
           crit_mass = critical_limit(a, e, stell_luminosity_ratio);
           accrete_dust(mass, dust_mass, gas_mass, a, e, crit_mass, planet_inner_bound, planet_outer_bound);

           dust_mass += PROTOPLANET_MASS;

           if (mass > PROTOPLANET_MASS) coalesce_planetesimals(a, e, mass, crit_mass,dust_mass, gas_mass, stell_luminosity_ratio, planet_inner_bound, planet_outer_bound, do_moons);
           else if (flag_verbose & 0x0100)
             fprintf(stderr, ".. failed due to large neighbor.\n");
         }
         else if (flag_verbose & 0x0200)
           fprintf(stderr, ".. failed.\n");
       }*/
      return planet_head;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="head"></param>
    private void free_dust(dust_record head)
    {
      dust_record node;
      dust_record next;
      /*
      for (node = head; node != NULL; node = next)
      {
        next = node.next_band;
        free(node);
      }
      */
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="head"></param>
    private void free_planet(planet_record head)
    {
      planet_record node;
      planet_record next;
      /*
      for (node = head; node != NULL; node = next)
      {
        next = node.next_planet;
        free(node);
      }
      */
    }

    /// <summary>
    /// 
    /// </summary>
    private void free_generations()
    {
      gen node;
      gen next;

      /* for (node = hist_head; node != NULL; node = next)
      {
        next = node.next;
        if (node.dusts) free_dust(node.dusts);
        if (node.planets) free_planet(node.planets);
        free(node);
      }
      if (dust_head != NULL) free_dust(dust_head);
      if (planet_head != NULL) free_planet(planet_head);
      dust_head = NULL;
      planet_head = NULL;
      hist_head = NULL;
      */
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="head"></param>
    private void free_atmosphere(planet_record head)
    {
      planet_record node;
      /*
      for (node = head; node != NULL; node = node.next_planet)
      {
        if (node.atmosphere != NULL)
        {
          free(node.atmosphere);

          node.atmosphere = NULL;
        }
        if (node.first_moon != NULL)
        {
          free_atmosphere(node.first_moon);
        }
      }
      */
    }









    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CreateSystemForm_Load(object sender, EventArgs e)
    {
      BackColor = Color.Black;
      labelEccentricityCoeff.ForeColor = Color.White;
      numericUpDownEccentricityCoeff.Value = (decimal)ECCENTRICITY_COEFF;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CreateSystemForm_Shown(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void buttonCreate_Click(object sender, EventArgs e)
    {
      MessageBox.Show(randomEccentricity(1, 0.1).ToString());
      MessageBox.Show(randomEccentricity(1, 0.1).ToString());
      MessageBox.Show(randomEccentricity(1, 0.1).ToString());
      MessageBox.Show(randomEccentricity(1, 0.1).ToString());
      MessageBox.Show(randomEccentricity(1, 0.1).ToString());
    }
  }
}
