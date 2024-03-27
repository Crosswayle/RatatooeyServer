using ProtoBuf.Meta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatatooeyServer.Helpers
{
	public class TypeRegistry
	{
		private static int _typeIndex = 0;

		public static void AddTypeToSerializer(Type parent, Type type)
		{
			if (type == null || parent == null)
				throw new ArgumentNullException();

			bool isAlreadyAdded = RuntimeTypeModel.Default[parent].GetSubtypes().Any(subType => subType.DerivedType.Type == type);

			if (!isAlreadyAdded)
				RuntimeTypeModel.Default[parent].AddSubType(++_typeIndex, type);
		}

		public static void AddTypesToSerializer(Type parent, params Type[] types)
		{
			foreach (Type type in types)
				AddTypeToSerializer(parent, type);
		}

		public static IEnumerable<Type> GetPacketTypes(Type type)
		{
			return AppDomain.CurrentDomain.GetAssemblies()
				.SelectMany(assembly => assembly.GetTypes())
				.Where(p => type.IsAssignableFrom(p) && !p.IsInterface);
		}
	}
}
