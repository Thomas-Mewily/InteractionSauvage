#pragma warning disable CS8981 // Le nom de type contient uniquement des caractères ascii en minuscules. De tels noms peuvent devenir réservés pour la langue.

// Same for Signed & Unsigned
// inative < (iptr == isize == index)  <= imax
// i8 < i16 < i32 < i64 ...

global using i8 = System.SByte;
global using i16 = System.Int16;
global using i32 = System.Int32;
global using i64 = System.Int64;
//global using i128 = System.Int128;

global using isize = System.IntPtr;
global using iptr = System.IntPtr;
global using imax = System.Int64;
global using inative = System.Int32;

// C# compatible
global using index = System.Int32;


global using u8 = System.Byte;
global using u16 = System.UInt16;
global using u32 = System.UInt32;
global using u64 = System.UInt64;

global using usize = System.UIntPtr;
global using uptr = System.UIntPtr;
global using umax = System.UInt64;
global using unative = System.UInt32;
//global using u128 = System.UInt128;


global using f32 = System.Single;
global using f64 = System.Double;
global using fmax = System.Double;

global using rune = System.Text.Rune;
#pragma warning restore CS8981 // Le nom de type contient uniquement des caractères ascii en minuscules. De tels noms peuvent devenir réservés pour la langue.
