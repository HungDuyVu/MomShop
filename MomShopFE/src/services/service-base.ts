export class ServiceBase{
    protected convertParamUrl(name: string, value: number | string | boolean | Date) {
		return name + "=" + encodeURIComponent("" + value) + "&";
	}
}