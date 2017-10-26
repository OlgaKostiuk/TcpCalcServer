function showResult()
{
	var x = form.xField.value;
	var y = form.yField.value;
	var op = form.opField.value;
	var res = calcN(Number(x),Number(y),op);
	form.resField.value = res;
}

function calculate(x,y,op)
{
	var result = 0;

	if(op === '+')
		result = x + y;
	else if(op === '-')
		result = x - y;
	else if(op === '*')
		result = x * y;
	else if(op === '/')
		result = x / y;		
		
	return result;
}

function calcN( a, b, op )
{
	if(op === '+')
		op = 'p';
	
	var param = "a="+a+"&b="+b+"&op="+op;
	var rr = new XMLHttpRequest();
	rr.open('POST', 'http://localhost:8888', false);
	rr.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
	rr.send(param);
	//if(xmlhttp.status == 200)
	return rr.responseText;
}